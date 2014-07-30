﻿using PrototypeEducom.Helper;
using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View.Customers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data.Entity;

namespace PrototypeEDUCOM.ViewModel.Customers
{
    class ListCustomerViewModel : BaseViewModel
    {

        public SortableObservableCollection<Contact> customers { get; set; }

        public int nbrCustomer 
        { 
            get { return this.customers.Count; } 
        }

        private Dictionary<string,bool> directionSorted = new Dictionary<string,bool>();

        public Dictionary<string, string> countries { get; set; }

        public Dictionary<string, string> languages { get; set; }

        public string filterCountry { get; set; }
        public string filterLanguage { get; set; }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdAdd { get; set; }

        public ICommand cmdFilter { get; set; }
        public ICommand cmdSort { get; set; }
     
        public ListCustomerViewModel() : base()
        {
            this.customers = new SortableObservableCollection<Contact>(db.contacts
                .Include(c => c.requests.Select(r => r.events.Select(e => e.event_types)))
                .Include(c => c.emails)
                .ToList());

            this.cmdViewDetail = new RelayCommand<Contact>(actViewDetail);
            this.cmdAdd = new RelayCommand<object>(actAdd);
            this.cmdFilter = new RelayCommand<object>(actFilter);
            this.cmdSort = new RelayCommand<string>(actSort);
            mediator.Register(Helper.Event.ADD_CUSTOMER, this);

            directionSorted.Add("firstname", false);
            directionSorted.Add("lastname", false);
            directionSorted.Add("country", false);
            directionSorted.Add("city", false);
            directionSorted.Add("language", false);
            directionSorted.Add("date", false);

            countries = new Dictionary<string, string>();
            countries.Add("all", "TOUS");
            countries = countries.Concat(Dictionaries.countries).ToDictionary(pair => pair.Key, pair => pair.Value);
            filterCountry = countries.First().Key;

            languages = new Dictionary<string, string>();
            languages.Add("all", "TOUS");
            languages = languages.Concat(Dictionaries.languages).ToDictionary(pair => pair.Key, pair => pair.Value);
            filterLanguage = languages.First().Key;
        }

        private void actAdd(object obj)
        {
            mediator.openAddCustomerView();
        }

        private void actFilter(object obj)
        {
            var query = from p in db.contacts
                        select p;

            if (filterCountry != "all")
                query = query.Where(c => c.country == filterCountry);

            if (filterLanguage != "all")
                query = query.Where(c => c.language == filterLanguage);

            this.customers = new SortableObservableCollection<Contact>(query.ToList());

            NotifyPropertyChanged("customers");
            NotifyPropertyChanged("nbrCustomer");
        }

        private void actSort(string arg)
        {

            ListSortDirection direction;

            direction = directionSorted[arg] ? ListSortDirection.Descending : ListSortDirection.Ascending;
            directionSorted[arg] = !directionSorted[arg];

            switch (arg)
            {
                case "firstname" :
                    this.customers.Sort(c => c.firstname, direction);
                    break;
                case "lastname":
                    this.customers.Sort(c => c.lastname, direction);
                    break;
                case "country":
                    this.customers.Sort(c => c.country, direction);
                    break;
                case "city":
                    this.customers.Sort(c => c.city, direction);
                    break;
                case "language":
                    this.customers.Sort(c => c.language, direction);
                    break;
                case "date":
                    this.customers.Sort(c => c.add_date, direction);
                    break;
            }
            
            NotifyPropertyChanged("customers");
        }

        public void actViewDetail(Contact customer)
        {
            mediator.openShowCustomerView(customer);
            mediator.Register(Helper.Event.DELETE_CUSTOMER, this);
            mediator.Register(Helper.Event.DELETE_CUSTOMER, mediator.mainTabs[TabName.CUSTOMER].tabViewModel);
        }

        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_CUSTOMER :

                    // Ajoute dans la liste
                    this.customers.Add((Contact)item);
                    NotifyPropertyChanged("customers");
                    NotifyPropertyChanged("nbrCustomer");
                    break;
                case Helper.Event.DELETE_CUSTOMER :
                    // Ajoute dans la liste
                    this.customers.Remove((Contact)item);
                    NotifyPropertyChanged("customers");
                    NotifyPropertyChanged("nbrCustomer");
                    break;
            }
        }
    }
}
