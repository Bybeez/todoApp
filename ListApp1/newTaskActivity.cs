using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ListApp1
{
    [Activity(Label = "newTaskActivity")]
    public class newTaskActivity : Activity
    {
        private EditText name;
        private Spinner priority;
        private EditText description;
        private Button save;
        private Button cancel;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.newTask);

            name = FindViewById<EditText>(Resource.Id.editName);
            priority = FindViewById<Spinner>(Resource.Id.editPriority);
            description = FindViewById<EditText>(Resource.Id.editDesc);
            save = FindViewById<Button>(Resource.Id.saveButton);
            cancel = FindViewById<Button>(Resource.Id.cancelButton);

            List<string> priorities = new List<string>(Enum.GetNames(typeof(PriorityEnum)));
            priority.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, priorities);

            Intent i = new Intent(this, typeof(newTaskActivity));


            save.Click += delegate {
                Bundle b = new Bundle();
                Log.Debug("click", priority.SelectedItem.ToString());
                b.PutString("name", name.Text);
                b.PutInt("priority", priority.SelectedItemPosition);
                b.PutString("description", description.Text);
                i.PutExtras(b);
                SetResult(Result.Ok, i);
                Finish();
            };

            cancel.Click += delegate
            {
                SetResult(Result.Canceled, i);
                Finish();
            };


        }
    }
}