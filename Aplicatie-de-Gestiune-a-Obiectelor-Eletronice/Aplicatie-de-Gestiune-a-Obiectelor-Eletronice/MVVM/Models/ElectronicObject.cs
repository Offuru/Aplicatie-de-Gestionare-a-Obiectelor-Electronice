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

        public enum ActiveObjectType
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

        private static List<string> classrooms = new List<string>
        { "PI1", "PI2", "PII1","PII2","PII3","PII4","PII5","PII6",
            "PII7","PIII1", "PIII2", "PIII4", "alta" };

        private int _id;
        private ObjectType _type;
        private ActiveObjectType _activeObjectType;
        private string _code;
        private int _order;
        private string _receiptNumber;
        private string _date;
        private string _name;
        private string _serial;
        private string _destination;
        private string _receiverName;

        public int Id { get => _id; set => _id = value; }
        public ObjectType Type { get => _type; set => _type = value; }
        public ActiveObjectType ActiveObjectType1 { get => _activeObjectType; set => _activeObjectType = value; }
        public string Code { get => _code; set => _code = value; }
        public int Order { get => _order; set => _order = value; }
        public string ReceiptNumber { get => _receiptNumber; set => _receiptNumber = value; }
        public string Date { get => _date; set => _date = value; }
        public string Name { get => _name; set => _name = value; }
        public string Serial { get => _serial; set => _serial = value; }
        public string Destination { get => _destination; set => _destination = value; }
        public string ReceiverName { get => _receiverName; set => _receiverName = value; }
    }
}
