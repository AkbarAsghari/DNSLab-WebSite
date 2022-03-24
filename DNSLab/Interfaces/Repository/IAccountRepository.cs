﻿using DNSLab.DTOs.User;

namespace DNSLab.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task<UserInfo> Get();
        Task<string> Login(AuthenticateDTO userInfo);
        Task<string> Register(UserInfo userInfo);
        Task<bool> Update(UserInfo userInfo);
        Task<int> UsersCount();
    }
}
