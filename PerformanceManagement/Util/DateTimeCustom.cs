using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;
using System.Drawing.Imaging;

namespace PerformanceManagement.Util
{
    public class DateTimeCustom
    {
        public string MiladiToShamsi(DateTime? date)
        {
            if (date != null)
            {
                var _date = Convert.ToDateTime(date);
                PersianCalendar pc = new PersianCalendar();
                try
                {
                    string now = "";
                    string m = pc.GetMonth(_date) <= 9 ? "0" + pc.GetMonth(_date).ToString() : pc.GetMonth(_date).ToString();
                    string d = pc.GetDayOfMonth(_date) <= 9 ? "0" + pc.GetDayOfMonth(_date).ToString() : pc.GetDayOfMonth(_date).ToString();
                    now = pc.GetYear(_date).ToString() + "/" + m + "/" + d;
                    return now + " " + _date.ToString("HH:mm:ss");
                }
                catch { return ""; }
            }
            else
            {
                return "";
            }

        }

        public DateTime Shamsi2Miladi(string _date)
        {
            int year = int.Parse(_date.Substring(0, 4));
            int month = int.Parse(_date.Substring(5, 2));
            int day = int.Parse(_date.Substring(8, 2));
            PersianCalendar p = new PersianCalendar();
            DateTime date = p.ToDateTime(year, month, day, 0, 0, 0, 0);
            return date;
        }

        public string FullShamsiDate(DateTime _date)
        {
            PersianCalendar pc = new PersianCalendar();
            try
            {
                string now = "";
                string weekDay = "";
                switch (pc.GetDayOfWeek(_date).ToString())
                {
                    case "Saturday":
                        weekDay = "شنبه";
                        break;
                    case "Sunday":
                        weekDay = "یکشنبه";
                        break;
                    case "Monday":
                        weekDay = "دوشنبه";
                        break;
                    case "Tuesday":
                        weekDay = "سه شنبه";
                        break;
                    case "Wednesday":
                        weekDay = "چهار شنبه";
                        break;
                    case "Thursday":
                        weekDay = "پنج شنبه";
                        break;
                    case "Friday":
                        weekDay = "جمعه";
                        break;
                }
                string month = "";
                switch (pc.GetMonth(_date))
                {
                    case 1:
                        month = "فروردین ماه";
                        break;
                    case 2:
                        month = "اردیبهشت ماه";
                        break;
                    case 3:
                        month = "خرداد ماه";
                        break;
                    case 4:
                        month = "تیر ماه";
                        break;
                    case 5:
                        month = "مرداد ماه";
                        break;
                    case 6:
                        month = "شهریور ماه";
                        break;
                    case 7:
                        month = "مهر ماه";
                        break;
                    case 8:
                        month = "آبان ماه";
                        break;
                    case 9:
                        month = "آذر ماه";
                        break;
                    case 10:
                        month = "دی ماه";
                        break;
                    case 11:
                        month = "بهمن ماه";
                        break;
                    case 12:
                        month = "اسفند ماه";
                        break;
                }

                string d = pc.GetDayOfMonth(_date) <= 9 ? "0" + pc.GetDayOfMonth(_date).ToString() : pc.GetDayOfMonth(_date).ToString();
                now = weekDay + " " + d + " " + month + " " + pc.GetYear(_date).ToString();
                return now;
            }
            catch { return ""; }
        }

        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool TimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            // convert datetime to a TimeSpan
            TimeSpan now = datetime.TimeOfDay;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }

        public bool BeetweenDate(string key, string start, string end)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || key == "")
                {
                    return false;
                }
                if (string.Compare(key, start) >= 0 && string.Compare(key, end) <= 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsNumeric(string Value)
        {
            double n;
            bool isNumeric = double.TryParse(Value, out n);
            return isNumeric;
        }
    }
}
