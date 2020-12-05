using System.Collections.Generic;

public class OkHall
{
    public List<User> Users { get; set; }

    public JukeBox JukeBox { get; set; }

    public void JoinUser(User user)
    {
        Users.Add(user);
    }

    public void LeaveUser(User user)
    {
        Users.RemoveAll(x => x.Id == user.Id);
    }

    public void ChooseMusic(Music music)
    {
        JukeBox.PushMusic(music);
    }
}

///<summary>
/// 点唱机
///</summary>
public class JukeBox
{
    private Stack<Music> Playlist { get; set; }

    public Music Current { get; set; }

    public void PushMusic(Music music)
    {
        Playlist.Push(music);
    }

    public void Next()
    {
        // 移除当前
        Playlist.Pop();
        Current = Playlist.Peek();
    }

    public void OnCurrentEnd()
    {
        Next();
    }
}