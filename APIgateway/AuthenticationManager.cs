﻿using APIgateway.Controllers;
using APIgateway.usermethod;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIgateway
{

    public class AuthenticationManager : IAurthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>();
        private readonly string key;
        public AuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Aurthenticate(string username, string password)
        {
            users.Clear();
            users.Add(username, password);
            if (!users.Any(p => p.Key == username && p.Value == password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF32.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
