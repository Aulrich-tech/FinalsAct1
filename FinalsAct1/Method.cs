using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class TransactionGenerator
{
    public static string Generate()
    {
        string date = DateTime.Now.ToString("yyyyMMdd");
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();

        char[] transactionNumber = new char[6];
        for (int i = 0; i < transactionNumber.Length; i++)
        {
            transactionNumber[i] = chars[random.Next(chars.Length)];
        }

        return date + "-" + new string(transactionNumber);
    }
}
public static class TicketPrice
{
    public static decimal GetTicketPrice(string ticketClass)
    {
        switch (ticketClass)
        {
            case "Upper Box - 200":
                return 200m;
            case "Lower Box - 400":
                return 400m;
            case "Gen Ad - 100":
                return 100m;
            default:
                throw new ArgumentException("Invalid ticket class");
        }
    }
}
public static class NoText
{
    public static bool Validation(TextBox tbcs, TextBox tbaddress, TextBox tbtix,
        TextBox tbcontact, TextBox tbemail, ComboBox cbevent, ComboBox cbtix)
    {
        if (string.IsNullOrWhiteSpace(tbcs.Text))
        {
            MessageBox.Show("Please enter the customer's name.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tbcs.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(tbtix.Text))
        {
            MessageBox.Show("Please enter the ticket number.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tbtix.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(tbaddress.Text))
        {
            MessageBox.Show("Please enter the address.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tbaddress.Focus();
            return false;
        }

        for (int i = 0; i < tbcontact.Text.Length; i++)
        {
            if (!char.IsDigit(tbcontact.Text[i]))
            {
                MessageBox.Show("Contact number should contain only digits.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbcontact.Focus();
                return false;
            }
        }

        if (string.IsNullOrWhiteSpace(tbemail.Text))
        {
            MessageBox.Show("Please enter the email address.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tbemail.Focus();
            return false;
        }

        if (cbevent.SelectedIndex < 0)
        {
            MessageBox.Show("Please select an event.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cbevent.Focus();
            return false;
        }

        if (cbtix.SelectedIndex < 0)
        {
            MessageBox.Show("Please select a ticket type.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cbtix.Focus();
            return false;
        }
        // validations for specific data//
        if (tbcontact.Text.Length != 11)
        {
            MessageBox.Show("Contact number should be exactly 11 digits long.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tbcontact.Focus();
            return false;
        }
        foreach (char c in tbcs.Text)
        {
            if (!char.IsLetter(c))
            {
                MessageBox.Show("Customer name should only be letters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbcs.Focus();
                return false;
            }
        }
        foreach (char c in tbemail.Text)
        {
            if (!tbemail.Text.Contains("@") || !tbemail.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbemail.Focus();
                return false;
            }
        }
        foreach (char c in tbtix.Text)
        {
            if (!char.IsDigit(c))
            {
                MessageBox.Show("Ticket number should contain only digits.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbtix.Focus();
                return false;
            }
        }
        return true;

    }
}

