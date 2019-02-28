using System;
namespace ObserverSubject
{
    public interface IObserver
    {
        void Update(Subject subject);
    }
}
