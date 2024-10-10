﻿using Encuba.Ejemplo.Domain.Entities;
using Encuba.Ejemplo.Domain.Interfaces.Repositories;
using Encuba.Ejemplo.Domain.Seed;
using Express.Trades.Security.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Encuba.Ejemplo.Infrastructure.Repositories;

public class UserPublicAccessTokenRepository(
    SecurityDbContext dbContext) : IUserPublicAccessTokenRepository
{
    public IUnitOfWork UnitOfWork => dbContext;
    

    public UserPublicAccessToken Add(UserPublicAccessToken userPublicAccessToken)
    {
        return dbContext.UserPublicAccessTokens.Add(userPublicAccessToken).Entity;
    }

    public void Delete(List<UserPublicAccessToken> userPublicAccessTokens)
    {
        dbContext.UserPublicAccessTokens.RemoveRange(userPublicAccessTokens);
    }
    
    public async Task<UserPublicAccessToken> GetByUserIdAsync(Guid userId)
    {
        return (await dbContext.UserPublicAccessTokens
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.PublicAccessTokenId == userId))!;
    }
}