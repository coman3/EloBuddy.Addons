using System.Collections.Generic;
using System.Linq;

namespace CameraBuddy.Spectate.Extensions
{
    public class SmartList<TItem> : List<TItem>
    {
        public static SmartList<TItem> operator +(SmartList<TItem> item1, SmartList<TItem> item2)
        {
            var list = new SmartList<TItem>();
            list.AddRange(item1);
            foreach (var item in item2)
            {
                if(!list.Any(x => x.Equals(item2))) list.Add(item);
            }
            return list;
        }
        public static SmartList<TItem> operator -(SmartList<TItem> item1, SmartList<TItem> item2)
        {
            var list = new SmartList<TItem>();
            list.AddRange(item1);
            foreach (var item in item2)
            {
                if (list.Any(x => x.Equals(item2))) list.Remove(item);
            }
            return list;
        }
    }
}