using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

using ListApp1.UI;
using ListApp1.Core;
using Android.Content;
using Android.Util;
using Realms;
using System.Linq;

namespace ListApp1
{
    [Activity(Label = "ListApp1", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView listView;
        private List<ToDo> toDos;
        private ToDoAdapter adapter;
        private Button newTask;
        private Realm realm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListActivity);

            listView = FindViewById<ListView>(Resource.Id.listView);
            newTask = FindViewById<Button>(Resource.Id.newTaskButton);

            realm = Realm.GetInstance();

            toDos = realm.All<ToDo>().ToList<ToDo>();

            adapter = new ToDoAdapter(this, toDos);
            listView.Adapter=adapter;

            Intent i = new Intent(this, typeof(newTaskActivity));

            newTask.Click += delegate
            {
                this.StartActivityForResult(i, 0);
            };

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Log.Debug("log", data.ToString());
                
                toDos.Add(new ToDo {
                        Name = data.Extras.GetString("name"), 
                        Description = data.Extras.GetString("description"),
                        Priority = data.Extras.GetInt("priority"),
                });
                realm.Write(() =>
                {
                    realm.Add( new ToDo {
                        Name = data.Extras.GetString("name"),
                        Description = data.Extras.GetString("description"),
                        Priority = data.Extras.GetInt("priority"),
                    });
                });
                adapter.NotifyDataSetChanged();
            }
        }
    }
}

