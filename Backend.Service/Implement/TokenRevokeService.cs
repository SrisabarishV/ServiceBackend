using Backend.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Implement
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private static readonly HashSet<string> _blacklist = new();

        public void Revoke(string token)
        {
            _blacklist.Add(token);
        }

        public bool IsRevoked(string token)
        {
            return _blacklist.Contains(token);
        }
    }
}
