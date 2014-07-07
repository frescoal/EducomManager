﻿using PrototypeEDUCOM.Helper.Enum;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class AddStudentViewModel : BaseViewModel
    {
        public contact customer { get; set; }
        public String firstname { get; set; }
        public DateTime birthday { get; set; }
        public String lastname { get; set; }
        public List<Gender> genders { get; set; }
        public int genderIndex { get; set; }
        public List<Kinship> kinships { get; set; }
        public int kinshipIndex { get; set; }
        public String gender { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }
        public ShowCustomerViewModel parentVM { get; set; }


        public AddStudentViewModel(contact customer, ShowCustomerViewModel parentVM) {

            this.customer = customer;
            this.birthday = DateTime.Now;
            this.kinships = Kinship.list;
            this.genders = Gender.list;
            this.cmdAdd = new RelayCommand<object>(actAdd);
            this.parentVM = parentVM;
        }
        public void actAdd(object o)
        { 
            student student = new student();
            student.firstname = firstname;
            student.lastname = lastname;
            student.kinship = kinships.ElementAt(kinshipIndex).getValue();
            student.birthday = birthday;
            student.gender = genders.ElementAt(genderIndex).getValue();
            customer.students.Add(student);

            db.SaveChanges();

            parentVM.students.Add(student);
            parentVM.NotifyPropertyChanged("students");

            this.CloseActionAdd();
        }
    }
}
