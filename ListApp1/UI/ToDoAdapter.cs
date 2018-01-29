using System;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using ListApp1.Core;

namespace ListApp1.UI
{
    class ToDoAdapter : ArrayAdapter<ToDo>
    {
        private int resourceLayout = Resource.Layout.itemToDo;
        private List<ToDo> items;

        public ToDoAdapter(Context context, List<ToDo> objects) : base(context, 0, objects)
        {
            items = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(Context).Inflate(resourceLayout, null, false);
            }
            ToDo currentItem = GetItem(position);

            TextView name = convertView.FindViewById<TextView>(Resource.Id.name);
            TextView description = convertView.FindViewById<TextView>(Resource.Id.description);
            TextView priority = convertView.FindViewById<TextView>(Resource.Id.priority);

            name.Text = currentItem.Name;
            description.Text = currentItem.Description;
            priority.Text = getPriorityValueFromInt(currentItem.Priority);

            priority.SetTextColor(Context.Resources.GetColor(getItemColorPriority(currentItem.Priority)));

            return convertView;
        }

        public new ToDo GetItem(int position)
        {
            return items[position];
        }

        public string getPriorityValueFromInt(int position)
        {
            return Enum.GetNames(typeof(PriorityEnum))[position];
        }

        public override int Count => items.Count;
        
        private int getItemColorPriority(int priority)
        {
            Log.Debug("Color", priority.ToString());
            switch (priority)
            {
                case 0:
                    return Resource.Color.priorityHigh;
                case 1:
                    return Resource.Color.priorityNormal;
                case 2:
                    return Resource.Color.priorityLow;
                default:
                    return Resource.Color.priorityNormal;
            }
        }
    }
}