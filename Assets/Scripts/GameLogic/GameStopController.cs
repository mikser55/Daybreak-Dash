using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GameStopController : MonoBehaviour
{
    private readonly List<PauseSource> _sources = new();

    private void Awake()
    {
        MessageBroker.Default
            .Receive<PauseSource>()
            .Subscribe(Stop)
            .AddTo(this);

        MessageBroker.Default
            .Receive<PlaySource>()
            .Subscribe(Play)
            .AddTo(this);
    }

    public void Play(PlaySource source)
    {
        _sources.Remove(_sources.FirstOrDefault(x => x.Key == source.Key));

        if (_sources.Count > 0)
            return;

        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void Stop(PauseSource source)
    {
        foreach(PauseSource _source in _sources)
            if (_source.Key == source.Key)
                return;

        _sources.Add(source);

        Time.timeScale = 0;
        AudioListener.pause = true;
    }
}