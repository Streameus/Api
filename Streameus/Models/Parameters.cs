using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streameus.Models
{
    [ComplexType]
    public partial class Parameters
    {
        public bool NotifMail { get; set; }
    }
}