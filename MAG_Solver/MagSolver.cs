using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAG_Solver
{
    /// <summary>
    /// マグマグガンバのキーから解のリストを取得する計算機クラス
    /// </summary>
    public static class MagSolver
    {
        public static List<MagProbremsAnswer> Solve(MagProbremsKey key)
        {
            List<MagProbremsAnswer> answers = new List<MagProbremsAnswer>();

            MagProbremsAnswer answer1 = new MagProbremsAnswer();
            answer1.MiddleCenterValue = key.CenterValue;

            //★1
            for (int upperCenterValue = 1; upperCenterValue <= 9; upperCenterValue++)
            {
                MagProbremsAnswer answer2 = answer1.Clone();
                answer2.UpperCenterValue = upperCenterValue;
                if (ContainsDoubledValueIn(answer2)) continue;

                //★2
                for (int lowerCenterValue = 1; lowerCenterValue <= 9; lowerCenterValue++)
                {
                    MagProbremsAnswer answer3 = answer2.Clone();
                    answer3.LowerCenterValue = lowerCenterValue;
                    if (ContainsDoubledValueIn(answer3)) continue;

                    //◇1中縦
                    if (answer3.KeyValueOf(MagProbremKeyPosition.CenterVartical) != key.CenterVarticalKey)
                        continue;

                    //★3
                    for (int middleLeftValue = 1; middleLeftValue <= 9; middleLeftValue++)
                    {
                        MagProbremsAnswer answer4 = answer3.Clone();
                        answer4.MiddleLeftValue = middleLeftValue;
                        if (ContainsDoubledValueIn(answer4)) continue;
                        
                        //★4
                        for (int middleRightValue = 1; middleRightValue <= 9; middleRightValue++)
                        {
                            MagProbremsAnswer answer5 = answer4.Clone();
                            answer5.MiddleRightValue = middleRightValue;
                            if (ContainsDoubledValueIn(answer5)) continue;

                            //◇2中横
                            if (answer5.KeyValueOf(MagProbremKeyPosition.MiddleHolizontal) != key.MiddleHolizontalKey)
                                continue;

                            //★5
                            for (int upperLeftValue = 1; upperLeftValue <= 9; upperLeftValue++)
                            {
                                MagProbremsAnswer answer6 = answer5.Clone();
                                answer6.UpperLeftValue = upperLeftValue;
                                if (ContainsDoubledValueIn(answer6)) continue;

                                //★6
                                for (int lowerLeftValue = 1; lowerLeftValue <= 9; lowerLeftValue++)
                                {
                                    MagProbremsAnswer answer7 = answer6.Clone();
                                    answer7.LowerLeftValue = lowerLeftValue;
                                    if (ContainsDoubledValueIn(answer7)) continue;

                                    //◇3左縦
                                    if (answer7.KeyValueOf(MagProbremKeyPosition.LeftVartical) != key.LeftVarticalKey)
                                        continue;

                                    //★7
                                    for (int upperRightValue = 1; upperRightValue <= 9; upperRightValue++)
                                    {
                                        MagProbremsAnswer answer8 = answer7.Clone();
                                        answer8.UpperRightValue = upperRightValue;
                                        if (ContainsDoubledValueIn(answer8)) continue;

                                        //◇4上横
                                        if (answer8.KeyValueOf(MagProbremKeyPosition.UpperHolizontal) != key.UpperHolizontalKey)
                                            continue;

                                        //★8
                                        for (int lowerRightValue = 1; lowerRightValue <= 9; lowerRightValue++)
                                        {
                                            MagProbremsAnswer answer9 = answer8.Clone();
                                            answer9.LowerRightValue = lowerRightValue;
                                            if (ContainsDoubledValueIn(answer9)) continue;

                                            //◇5右縦
                                            if (answer9.KeyValueOf(MagProbremKeyPosition.RightVartical) != key.RightVarticalKey)
                                                continue;

                                            //◇6下横
                                            if (answer9.KeyValueOf(MagProbremKeyPosition.LowerHolizontal) != key.LowerHolizontalKey)
                                                continue;

                                            //全チェック通過した候補を回答リストに追加
                                            answers.Add(answer9);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return answers;
        }

        private static bool ContainsDoubledValueIn(MagProbremsAnswer target)
        {
            List<int> existsNumbers = new List<int>();
            foreach(int targetElement in target.Array)
            {
                if (targetElement == 0) continue;
                if (existsNumbers.Contains(targetElement))
                {
                    return true;
                }
                else
                {
                    existsNumbers.Add(targetElement);
                }
            }
            return false;
        }
    }

    public class MagProbremsAnswer
    {
        public int[,] Array;
        public int UpperLeftValue { get { return Array[0, 0]; } set { Array[0, 0] = value; } }
        public int UpperCenterValue { get { return Array[0, 1]; } set { Array[0, 1] = value; } }
        public int UpperRightValue { get { return Array[0, 2]; } set { Array[0, 2] = value; } }
        public int MiddleLeftValue { get { return Array[1, 0]; } set { Array[1, 0] = value; } }
        public int MiddleCenterValue { get { return Array[1, 1]; } set { Array[1, 1] = value; } }
        public int MiddleRightValue { get { return Array[1, 2]; } set { Array[1, 2] = value; } }
        public int LowerLeftValue { get { return Array[2, 0]; } set { Array[2, 0] = value; } }
        public int LowerCenterValue { get { return Array[2, 1]; } set { Array[2, 1] = value; } }
        public int LowerRightValue { get { return Array[2, 2]; } set { Array[2, 2] = value; } }

        public MagProbremsAnswer()
        {
            Array = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        }

        public MagProbremsAnswer(int upperLeft, int upperCenter, int upperRight,
                                 int middleLeft, int middleCenter, int middleRight,
                                 int lowerLeft, int lowerCenter, int lowerRight) : this()
        {
            UpperLeftValue = upperLeft;
            UpperCenterValue = upperCenter;
            UpperRightValue = upperRight;

            MiddleLeftValue = middleLeft;
            MiddleCenterValue = middleCenter;
            MiddleRightValue = middleRight;

            LowerLeftValue = lowerLeft;
            LowerCenterValue = lowerCenter;
            LowerRightValue = lowerRight;
        }

        public MagProbremsAnswer Clone()
        {
            return new MagProbremsAnswer(
                    UpperLeftValue,
                    UpperCenterValue,
                    UpperRightValue,
                    MiddleLeftValue,
                    MiddleCenterValue,
                    MiddleRightValue,
                    LowerLeftValue,
                    LowerCenterValue,
                    LowerRightValue
                );
        }

        public int ValueOf(MagProbremPosition position)
        {
            int returnValue = 0;
            switch(position)
            {
                case MagProbremPosition.UpperLeft:    returnValue = UpperLeftValue;    break;
                case MagProbremPosition.UpperCenter:  returnValue = UpperCenterValue;  break;
                case MagProbremPosition.UpperRight:   returnValue = UpperRightValue;   break;
                case MagProbremPosition.MiddleLeft:   returnValue = MiddleLeftValue;   break;
                case MagProbremPosition.MiddleCenter: returnValue = MiddleCenterValue; break;
                case MagProbremPosition.MiddleRight:  returnValue = MiddleRightValue;  break;
                case MagProbremPosition.LowerLeft:    returnValue = LowerLeftValue;    break;
                case MagProbremPosition.LowerCenter:  returnValue = LowerCenterValue;  break;
                case MagProbremPosition.LowerRight:   returnValue = LowerRightValue;   break;
            }
            return returnValue;
        }

        public int TotalValueOf(MagProbremKeyPosition position)
        {
            int total = 0;
            switch(position)
            {
                case MagProbremKeyPosition.UpperHolizontal:
                    total = UpperLeftValue + UpperCenterValue + UpperRightValue; break;

                case MagProbremKeyPosition.MiddleHolizontal:
                    total = MiddleLeftValue + MiddleCenterValue + MiddleRightValue; break;

                case MagProbremKeyPosition.LowerHolizontal:
                    total = LowerLeftValue + LowerCenterValue + LowerRightValue; break;

                case MagProbremKeyPosition.LeftVartical:
                    total = UpperLeftValue + MiddleLeftValue + LowerLeftValue; break;

                case MagProbremKeyPosition.CenterVartical:
                    total = UpperCenterValue + MiddleCenterValue + LowerCenterValue; break;

                case MagProbremKeyPosition.RightVartical:
                    total = UpperRightValue + MiddleRightValue + LowerRightValue; break;
            }

            return total;
        }

        public int KeyValueOf(MagProbremKeyPosition position)
        {
            return TotalValueOf(position) % 10;
        }
    }

    public class MagProbremsKey
    {
        public int CenterValue;
        public int UpperHolizontalKey; public int MiddleHolizontalKey; public int LowerHolizontalKey;
        public int LeftVarticalKey; public int CenterVarticalKey; public int RightVarticalKey;

        public MagProbremsKey(int Key)
        {
            string keyString = Key.ToString();

            CenterValue         = keyString.Length > 0 ? int.Parse(keyString[0].ToString()) : 0;
            UpperHolizontalKey  = keyString.Length > 1 ? int.Parse(keyString[1].ToString()) : 0;
            MiddleHolizontalKey = keyString.Length > 2 ? int.Parse(keyString[2].ToString()) : 0;
            LowerHolizontalKey  = keyString.Length > 3 ? int.Parse(keyString[3].ToString()) : 0;
            LeftVarticalKey     = keyString.Length > 4 ? int.Parse(keyString[4].ToString()) : 0;
            CenterVarticalKey   = keyString.Length > 5 ? int.Parse(keyString[5].ToString()) : 0;
            RightVarticalKey    = keyString.Length > 6 ? int.Parse(keyString[6].ToString()) : 0;
        }
    }


    public enum MagProbremPosition
    {
        UpperLeft,
        UpperCenter,
        UpperRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        LowerLeft,
        LowerCenter,
        LowerRight,
    }

    public enum MagProbremKeyPosition
    {
        UpperHolizontal,
        MiddleHolizontal,
        LowerHolizontal,
        LeftVartical,
        CenterVartical,
        RightVartical,
    }
}
