using System;
using System.Collections.Generic;

public class UserDomain
{
    private readonly IMusicRepository _musicRepository;

    public UserDomain(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }

    public Guid Id { get; set; }

    public string UserName { get; set; }

    public List<Music> MyMusics { get; set; }

    public void Upload(Music music)
    {
        MyMusics.Add(music);
    }

    public byte[] Download(Guid musicId)
    {
        var music = MyMusics.Find(x => x.Id == musicId);
        byte[] musicContent = _musicRepository.GetMusic(music.FileName);
        return musicContent;
    }

    public List<Music> GetMusics()
    {
        return MyMusics;
    }
}