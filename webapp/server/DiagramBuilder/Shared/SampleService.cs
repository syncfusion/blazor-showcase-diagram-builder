using System;
using System.Threading.Tasks;

namespace DiagramBuilder.Shared
{
    public class SampleService
    {
        // Can be called from anywhere
        public void Update(NotifyProperties prop)
        {
            if (Notify != null)
            {
                this.Prop = prop;
                Notify.Invoke(prop);
            }
        }

        public NotifyProperties Prop
        {
            get; set;
        } = new NotifyProperties();

        public event Func<NotifyProperties, Task> Notify;
    }

    public class NotifyProperties
    {
        public bool HideSpinner { get; set; }

        public bool RestricMouseEvents { get; set; }
    }

}
