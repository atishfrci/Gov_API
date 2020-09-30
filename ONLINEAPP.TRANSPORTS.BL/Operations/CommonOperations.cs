using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class CommonOperations
    {
        private ReservedMarkOperation objReservedMark { get; set; }

        public CommonOperations()
        {
            this.objReservedMark = new ReservedMarkOperation();
        }


        public ReservedMark GetReservedMarksByMark(string RegistrationMark, string siteUrl, string token, string prefix)
        {
            try
            {
                return objReservedMark.ReservedRegistrationMark(RegistrationMark, siteUrl, token, prefix);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<string> GetReservedMasks(string siteUrl, string token, string prefix)
        {
            try
            {
                List<ReservedMark> _reservedMark = objReservedMark.GetReservedMarks(siteUrl, token , prefix);
                return _reservedMark.Select(r => r.RegistrationMark).ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        /// <summary>
        /// Get Registration mark as per rule.
        /// - Rule 1 "1 - 9999"
        /// - Rule 1 "A1 - Z2500"
        /// - Rule 1 "AA1 - FM2500"
        /// </summary>
        /// <param name="Rule">Send Rule Name</param>
        public List<string> GetRegistrationMarks(string Rule)
        {
            List<string> _registrationMarks = new List<string>();
            switch (Rule)
            {
                case "Rule1":
                    //_registrationMarks.AddRange(GetRegMarksUnderRule1()); // COMMENTED BY HEMA 18.11.2019 SINCE ONLY NEW RULE REQUIRED
                    break;
                case "Rule2":
                    // _registrationMarks.AddRange(GetRegMarksUnderRule2()); // COMMENTED BY HEMA 18.11.2019 SINCE ONLY NEW RULE REQUIRED
                    break;
                case "Rule3":
                    //_registrationMarks.AddRange(GetRegMarksUnderRule3()); // COMMENTED BY HEMA 18.11.2019 SINCE ONLY NEW RULE REQUIRED
                    break;
                case "Rule4":
                    _registrationMarks.AddRange(GetRegMarksUnderRule4());
                    break;
                default:
                    //_registrationMarks.AddRange(GetRegMarksUnderRule1()); // COMMENTED BY HEMA 18.11.2019 SINCE ONLY NEW RULE REQUIRED
                    //_registrationMarks.AddRange(GetRegMarksUnderRule2()); // COMMENTED BY HEMA 18.11.2019 SINCE ONLY NEW RULE REQUIRED
                    //_registrationMarks.AddRange(GetRegMarksUnderRule3()); // COMMENTED BY HEMA 18.11.2019 SINCE ONLY NEW RULE REQUIRED
                    _registrationMarks.AddRange(GetRegMarksUnderRule4());//DECOMMENTED BY HEMA 25.01.2019
                    break;
            }
            return _registrationMarks;
        }
        /// <summary>
        /// Get Registration Marks Under Rule 4 "FN1 - ZZ1000"
        /// </summary>
        /// <returns></returns>
        private List<string> GetRegMarksUnderRule4()
        {
            List<string> _registrationMarks = new List<string>();
            //int minCount = 1; int maxCount = 9999;
            int minCount = 1; /*int maxCount = 2500; COMMENTED BY HEMA NEW LIMIT 1000 */
            int maxCount = 1000; 
            string[] charOption1Rule4 = { "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] charOption2Rule4 = { "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] charOptionGeneral = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            int count = 1;
            foreach (string _char1 in charOption1Rule4)
            {
                if (charOption1Rule4.Count() == count)
                {
                    foreach (string _char in charOptionGeneral)
                    {
                        if (_char1 == "Z" && _char == "Z")
                        {
                            minCount = 1;  maxCount = 1000;
                            for (int Counter = minCount; Counter <= maxCount; Counter++)
                            {
                                _registrationMarks.Add(string.Concat(_char1, _char, Convert.ToString(Counter)));
                            }
                        }
                        else
                        {
                            for (int Counter = minCount; Counter <= maxCount; Counter++)
                            {
                                _registrationMarks.Add(string.Concat(_char1, _char, Convert.ToString(Counter)));
                            }
                        }
                       
                    }
                }
                else
                {
                    if (_char1 == "F")
                    {
                        foreach (string _char in charOption2Rule4)
                        {
                            for (int Counter = minCount; Counter <= maxCount; Counter++)
                            {
                                _registrationMarks.Add(string.Concat(_char1, _char, Convert.ToString(Counter)));
                            }
                        }
                    }
                    else
                    {
                        foreach (string _char in charOptionGeneral)
                        {
                            for (int Counter = minCount; Counter <= maxCount; Counter++)
                            {
                                _registrationMarks.Add(string.Concat(_char1, _char, Convert.ToString(Counter)));
                            }
                        }
                    }
                }
                count++;
            }
            return _registrationMarks;
        }

        /// <summary>
        /// Get Registration Marks Under Rule 3 "AA1 - FM2500"
        /// </summary>
        /// <returns></returns>
        private List<string> GetRegMarksUnderRule3()
        {
            List<string> _registrationMarks = new List<string>();
            //int minCount = 1; int maxCount = 9999;
            int minCount = 1; int maxCount = 2500;
            string[] charOption1Rule3 = { "A", "B", "C", "D", "E", "F" };
            string[] charOption2Rule3 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
            string[] charOptionGeneral = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            int count = 1;
            foreach (string _char1 in charOption1Rule3)
            {
               //commented by HEMA on 29.11.2018 if (charOption1Rule3.Count() == count)
                if (_char1.Equals('F'))
                {
                    foreach (string _char in charOption2Rule3)
                    {
                        for (int Counter = minCount; Counter <= maxCount; Counter++)
                        {
                            _registrationMarks.Add(string.Concat(_char1, _char, Convert.ToString(Counter)));
                        }
                    }
                }
                else
                {
                    foreach (string _char in charOptionGeneral)
                    {
                        for (int Counter = minCount; Counter <= maxCount; Counter++)
                        {
                            _registrationMarks.Add(string.Concat(_char1, _char, Convert.ToString(Counter)));
                        }
                    }
                }
                count++;
            }
            return _registrationMarks;
        }

        /// <summary>
        /// Get Registration Marks Under Rule 2 "A1 - AZ2500"
        /// </summary>
        /// <returns></returns>
        private List<string> GetRegMarksUnderRule2()
        {
            List<string> _registrationMarks = new List<string>();
            //int minCount = 1; int maxCount = 9999;
            int minCount = 1; int maxCount = 2500;
            string[] charOption1Rule2 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            foreach (string _char in charOption1Rule2)
            {
                for (int Counter = minCount; Counter <= maxCount; Counter++)
                {
                    _registrationMarks.Add(string.Concat(_char, Convert.ToString(Counter)));
                }
            }
            return _registrationMarks;
        }

        /// <summary>
        /// Get Registration Marks Under Rule "1 - 9999"
        /// </summary>
        /// <returns></returns>
        private List<string> GetRegMarksUnderRule1()
        {
            List<string> _registrationMarks = new List<string>();
            int minCount = 1; int maxCount = 9999;
            for (int Counter = minCount; Counter <= maxCount; Counter++)
            {
                _registrationMarks.Add(Convert.ToString(Counter));
            }
            return _registrationMarks;
        }

        /// <summary>
        /// Get Registration Marks with given range
        /// </summary>
        /// <returns></returns>
        public List<string> GetRegMarksWithinRange(string _char, int minCount, int maxCount)
        {
            List<string> _registrationMarks = new List<string>();
            for (int Counter = minCount; Counter <= maxCount; Counter++)
            {
                _registrationMarks.Add(string.Concat(_char, Convert.ToString(Counter)));
            }
            return _registrationMarks;
        }
    }
}
