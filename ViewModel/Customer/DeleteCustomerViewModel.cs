﻿using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class DeleteCustomerViewModel : BaseViewModel
    {

        public contact contact { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }

        public Action CloseActionDelete { get; set; }

        public DeleteCustomerViewModel(contact contact)
        {
            this.cmdDelete = new RelayCommand<Object>(actDeleteCustomer);
            this.contact = contact;
        }


        public void actDeleteCustomer(Object o) {

            db.contacts.Remove(contact);
            db.SaveChanges();
            this.CloseActionDelete();
        }
    }
}