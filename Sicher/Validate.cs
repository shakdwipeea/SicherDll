using Akant;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




/*
*Error occured = 402
 *Trial expired = 20
 *authorization failed = 403
 *authorizatioin complete = 200
 *License not active = 404
 *error writing registry = 401
*/
namespace Sicher
{
    public class Validate
    {  
        public int Start(String software)
        {

            try
            {
                software = software.ToLower();
                software = software.Replace(" ", "");
                Console.WriteLine(software);
                if (!checkRegistry(software))
                {
                    return 404;
                }
                Credentials c;
                try
                {
                    c = new Credentials();
                    c = JsonConvert .DeserializeObject<Credentials>(Encrypt.decrypt(Registry.readRegistry(software)));
                }
                catch (Exception)
                {
                    return 401;
                }
                if (c.count > 0 || c.count < 0)
                {
                    if (c.bios == Bios.getSerialNumber())
                    {

                        if (c.trial == 1)
                        {
                            c.count = -1;
                        }
                        else
                        {
                            c.count--;
                        }

                        try
                        {
                            //delete old registry
                            Registry.deleteRegistry(software);
                            Registry.createRegistry(Encrypt.encrypt(JsonConvert.SerializeObject(c)), software);

                            return 200;
                        }
                        catch (Exception)
                        {
                            return 401;
                        }
                    }
                    else
                    {
                        return 403;
                        // MessageBox.Show("Using Duplicate Key");
                        //Registry.deleteRegistry();
                        // Delete the Registry(Code to be added)
                    }
                }
                else if (c.count == 0)
                {
                    return 20;
                    // MessageBox.Show("Trial expired");
                    // new Entry().ShowDialog();
                }
                else
                {
                    return 402;
                }
            }
            catch (Exception)
            {
                return 402;
            }

        }

        Boolean checkRegistry(String software)   ///Check if the registery exists
        {
            try
            {
                string s = Registry.readRegistry(software);
                //MessageBox.Show(s);
                if (s != null)
                {
                    // MessageBox.Show("true");
                    return true;
                }

            }
            catch (Exception e)
            {
                // MessageBox.Show(e.ToString());
                return false;
            }
            // MessageBox.Show("false");
            return false;

        }


        public int getCount(String software)
        {
            try
            {
                software = software.ToLower();
                if (!checkRegistry(software))
                {
                    return -404;
                }
                Credentials c;
                try
                {
                    c = new Credentials();
                    c = JsonConvert.DeserializeObject<Credentials>(Encrypt.decrypt(Registry.readRegistry(software)));
                    return c.count;
                }
                catch (Exception)
                {
                    return -401;
                }

            }
            catch (Exception)
            {

                return -402;
            }

           
        }


    }
}



