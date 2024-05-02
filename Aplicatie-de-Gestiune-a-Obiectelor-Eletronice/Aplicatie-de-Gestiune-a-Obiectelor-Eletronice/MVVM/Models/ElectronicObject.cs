using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models
{
    public class ElectronicObject
    {
        public enum ObjectType
        {
            Active,
            Disposed,
            Proposed
        }

        public enum ActiveType
        {
            None,
            Inventory,
            Fixed
        }

        public enum Receiver
        {
            Other,
            Student,
            PhD,
            Teacher,
            Room
        }

        public static List<string> Classrooms = new List<string>
        { "PI1", "PI2", "PII1","PII2","PII3","PII4","PII5","PII6",
            "PII7","PIII1", "PIII2", "PIII4", "alta" };

        public int Id { get; set; }
        public string Type { get; set; }
        public string ActiveObjectType { get; set; }
        public string Code { get; set; }
        public string Order {  get; set; }
        public string ReceiptNumber { get; set; }
        public string Date {  get; set; }
        public string Name { get; set; }
        public string Serial {  get; set; }
        public string Destination { get; set; }
        public string ReceiverName { get; set; }

        public ElectronicObject()
        {
            Id = 0;
            Type = "";
            ActiveObjectType = "";
            Code = "";
            Order = "";
            ReceiptNumber = "";
            Date = "";
            Name = "";
            Serial = "";
            Destination = "";
            ReceiverName = "";
        }
    }
}
