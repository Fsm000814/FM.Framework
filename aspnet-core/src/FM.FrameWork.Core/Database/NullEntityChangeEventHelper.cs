using Abp.Events.Bus.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Database
{
    //
    // 摘要:
    //     Null-object implementation of Abp.Events.Bus.Entities.IEntityChangeEventHelper.
    public class NullEntityChangeEventHelper : IEntityChangeEventHelper
    {
        //
        // 摘要:
        //     Gets single instance of Abp.Events.Bus.Entities.NullEntityChangeEventHelper class.
        public static NullEntityChangeEventHelper Instance { get; } = new NullEntityChangeEventHelper();


        private NullEntityChangeEventHelper()
        {
        }

        public void TriggerEntityCreatingEvent(object entity)
        {
        }

        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {
        }

        public void TriggerEntityUpdatingEvent(object entity)
        {
        }

        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {
        }

        public void TriggerEntityDeletingEvent(object entity)
        {
        }

        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {
        }

        public void TriggerEvents(EntityChangeReport changeReport)
        {
        }

        public Task TriggerEventsAsync(EntityChangeReport changeReport)
        {
            return Task.FromResult(0);
        }
    }
}
