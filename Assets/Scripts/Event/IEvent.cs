using Cysharp.Threading.Tasks;
using System.Threading;

public interface IEvent
{
    public void Play(CancellationToken ct = default);
}
