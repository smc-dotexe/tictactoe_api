using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.ViewModels
{
    public class NewGameViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name length must be less than 20 characters")]
        public string PlayerOneName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name length must be less than 20 characters")]
        public string PlayerTwoName { get; set; }
    }
}
