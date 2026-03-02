using System.ComponentModel;

namespace StabSharp
{
    internal static class BindingListExtensions
    {
        public static void Move<T>(this BindingList<T> list, int oldIndex, int newIndex)
        {
            if (list == null)
            {
                return;
            }

            if (oldIndex == newIndex || oldIndex < 0 || newIndex < 0 || oldIndex >= list.Count || newIndex >= list.Count)
            {
                return;
            }

            T item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
        }
    }
}

