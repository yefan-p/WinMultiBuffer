using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;

namespace WebApiMultiBuffer.Models
{
    public class BuffersModel
    {
        public int Id { get; set; }

        public int User { get; set; }

        public int CopyKeyCode { get; set; }

        public int PasteKeyCode { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}