using Microsoft.IdentityModel.Tokens;
using Security.Common.Configuration;
using Security.Model.Dto;
using Security.Model.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security.Infraestructure.Adapter.Service
{
    public class SecurityService: ISecurityService
	{
		private IGeneralSettings _generalConfiguration;
		public SecurityService(IGeneralSettings generalConfiguration) {
			_generalConfiguration = generalConfiguration;
		}

		public string Encrypt(string Info)
		{
			string textEcrypt = EncryptMD5(Info);
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(textEcrypt);
			return System.Convert.ToBase64String(plainTextBytes); ;
		}

		public string GetToken(User oUser)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.ASCII.GetBytes(_generalConfiguration.GetKeyToken());
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, oUser.FullName,ClaimTypes.Name),
					new Claim(ClaimTypes.NameIdentifier,oUser.UserName,ClaimTypes.NameIdentifier),
					new Claim(ClaimTypes.Hash,oUser.UserName,ClaimTypes.Hash)
				}),
				Expires = DateTime.UtcNow.AddHours(_generalConfiguration.GetTimeToken()),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public TokenResultDto ValidateToken(string token)
		{
			try
			{
				JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
				TokenValidationParameters validationParameters = new TokenValidationParameters()
				{
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_generalConfiguration.GetKeyToken()))
				};
				SecurityToken oSecurityToken;
				var principal = tokenHandler.ValidateToken(token, validationParameters, out oSecurityToken);

				string name = principal.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name)?.Value;
				string userName = principal.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)?.Value;
				string code = principal.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Hash)?.Value;

				TokenResultDto oTokenResultDto = new TokenResultDto()
				{
					Name = name,
					UserName = userName,
					Code = code
				};

				return oTokenResultDto;
			}
			catch
			{
				return null;
			}
		}


		private string EncryptMD5(string text)
		{
			byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(text);
			MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
			byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_generalConfiguration.GetKeyPassword()));
			objMD5CryptoService.Clear();

			var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
			objTripleDESCryptoService.Key = securityKeyArray;
			objTripleDESCryptoService.Mode = CipherMode.ECB;
			objTripleDESCryptoService.Padding = PaddingMode.PKCS7;


			var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
			byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
			objTripleDESCryptoService.Clear();
			return Convert.ToBase64String(resultArray, 0, resultArray.Length);
		}

	}
}
