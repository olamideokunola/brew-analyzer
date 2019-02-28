using System;
using System.Collections.Generic;

namespace ObserverSubject
{
    public abstract class Subject
    {

        private IList<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            if(!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void Detach(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void Notify()
        {
            foreach(IObserver observer in observers)
            {
                observer.Update(this);
            }
        }

    }
}
