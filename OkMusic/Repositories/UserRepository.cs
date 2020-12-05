using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OkMusic.Models;

public class UserRepository
{
    private readonly OkMusicContext _db;

    public UserRepository(OkMusicContext db)
    {
        _db = db;
    }

    public void Update(User user)
    {
        _db.Users.Update(user);
    }

    internal User Get(Guid userId)
    {
        return _db.Users.FirstOrDefault(x => x.UserId == userId);
    }

    internal void Add(User user)
    {
        _db.Users.ad
    }
}
