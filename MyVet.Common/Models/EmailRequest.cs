using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVet.Common.Models
{
    public class EmailRequest
    {

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        public EmailRequest()
        {

        }



    }
}
