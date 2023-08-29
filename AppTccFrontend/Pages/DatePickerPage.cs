using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.Pages
{
    public class DatePickerPage : ContentPage
    {
        public DatePickerPage()
        {
            Title = "DatePicker Page";

            DatePicker datePicker = new DatePicker
            {
                MinimumDate = new DateTime(1900, 1, 1),
                MaximumDate = DateTime.Now,
                Date = DateTime.Now
            };

            Content = new StackLayout
            {
                Padding = new Thickness(20),
                Children =
                {
                    new Label
                    {
                        Text = "Selecione uma data:",
                        FontSize = 18,
                        Margin = new Thickness(0, 0, 0, 10)
                    },
                    datePicker
                }
            };
        }
    }
}
