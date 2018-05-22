using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class TCP_VAR
    {
        private const int I_MAX = 16;
        private const int O_MAX = 16;
        private bool[] I = new bool[I_MAX];
        private bool[] O = new bool[O_MAX];

        public bool get_bit(byte b,int position)
        {
            if (((b >> position) & 1) == 1)
                return true;
            else
                return false;
        }

        public byte set_bit(byte b, int positon)
        {
            return b|= (byte)(1 << positon);
        }
        public TCP_VAR(byte[] buff)
        {
            int j = 0;
            for (int i = 0; i < buff.Length && j < I_MAX; i++)
            {
                I[j++] = get_bit(buff[i], 7);
                I[j++] = get_bit(buff[i], 6);
                I[j++] = get_bit(buff[i], 5);
                I[j++] = get_bit(buff[i], 4);
                I[j++] = get_bit(buff[i], 3);
                I[j++] = get_bit(buff[i], 2);
                I[j++] = get_bit(buff[i], 1);
                I[j++] = get_bit(buff[i], 0);
               
            }
            for(int i=0;i<I_MAX;i++)
             System.Console.WriteLine("I["+i+"]= "+I[i]);
        }

        public byte[] Calculate_Results(int option)
        {
            O[0]= O[1] = O[2] = O[3] = O[4] = false;
            if(I[0])
            {
                return Convert_resullt();
            }
            if (I[5])
            {
                O[3] = I[3];
                O[4] = I[4];
                return Convert_resullt();
            }
            if (option == 1)
            {
                if (I[1] && I[2])
                {
                    return Convert_resullt();
                }
                else
                {
                    O[3] = I[3];
                    O[4] = I[4];
                    if (I[6] && I[4])
                    {
                        O[1] = I[1];
                        O[2] = I[2];
                    }
                    if (I[8] && I[3])
                    {
                        O[1] = I[1];
                        O[2] = I[2];
                    }
                    if (I[7] && I[4] && I[2])
                    {
                        O[2] = I[2];
                    }
                    if (I[7] && I[3] && I[1])
                    {
                        O[1] = I[1];
                    }
                }

            }
            else
                if (option == 2)
            {
                if ((I[6] && I[7]) || (I[6] && I[8]) || (I[7] && I[8]) || (I[6] &&I[7] && I[8]))
                {
                    O[0] = true;
                }
                else
                {
                    O[3] = I[3];
                    O[4] = I[4];
                    if (I[7])
                    {
                        if(I[3])
                        {
                            O[1] = I[1];
                        }
                        if (I[4])
                        {
                            O[2] = I[2];
                        }
                    }
                    else
                    {
                        if (I[1] && I[2])
                        {
                            return Convert_resullt();
                        }
                        else
                        {
                            
                            if (I[6] && I[4])
                            {
                                O[1] = I[1];
                                O[2] = I[2];
                            }
                            if (I[8] && I[3])
                            {
                                O[1] = I[1];
                                O[2] = I[2];
                            }
                           
                        }

                    }
                }

            }

            return Convert_resullt();
        }

        public byte[] Convert_resullt()
        {

            byte[] buff=new byte[O_MAX/8];
            int j = 0;
            for (int i=0;i<O.Length;i++)
            {
                System.Console.WriteLine("O[" + i + "]= " + O[i]);
                j = i / 8;
                if(O[i])
                {

                    
                    buff[j] = set_bit(buff[j], 7-(i % 8));
                }
                
            }

            System.Console.WriteLine(buff[0]);
            return buff;
        }
    }
}
