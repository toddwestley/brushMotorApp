using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;
using static BrushMotorApp.revolutions;
using Excel = Microsoft.Office.Interop.Excel;

namespace BrushMotorApp
{
    public partial class revolutions : Form
    {
        public revolutions()
        {
            InitializeComponent();
        }
        public struct konstants
        {
            public double functionPeriod,
                functionRampTime,
                functionMagnitude,
                functionBaseValue,
                speedNoLoad,
                stallTorque,
                inertiaRotor,
                inertiaExtra,
                mechanicalTimeConstant,
                noLoadCurrent,
                torqueConstant,
                motorViscousFrictionConstant,
                inertiaTotal,
                terminalResistance,
                terminalInductance,
                dataSheetVoltage,
                stallCurrent,
                pi,
                timeIncrement,
                timeSpan;
     
        }
        public struct dataPoint
        {
            public double time,
                position,
                speed,
                current;
        }
        public konstants konst;
        public double positionNow;
        public double velocityNow;
        public double currentNow;
        public double timeNow;
        public Boolean constantsLoaded = false;
        //public double timeIncrement = 0.0001;
        public Boolean constantsLoads = false;
        public LinkedList<dataPoint>dataPointslList = new LinkedList<dataPoint>();
        //public LinkedList<dataPoint>dataPointsListCopy = new LinkedList<dataPoint>();
        //public dataPoint dataPointArray[]; 
        public Boolean linkedListCreated = false;
        public double maxRevolution = 0;
        public dataPoint[] trialArray;
        public Boolean constantsRead = false;
        public void loadConstants(ref konstants konst)
        {
            Excel.Application excel_app = new Excel.ApplicationClass();
            excel_app.Visible = true;
            string xlFileName;// = "C:\\Users\\Todd Westley\\BrushMotorApplication\\BrushMotorApp\\excel file with only datsheet values.xlsx";
            xlFileName = Environment.CurrentDirectory + "\\excel file with only datsheet values.xlsx";
            Excel.Workbook workbook = excel_app.Workbooks.Open(xlFileName);
            Excel.Worksheet xlSheet = (Excel.Worksheet)workbook.Sheets[1];
            //konstants konst;
            konst.functionPeriod = (double)(xlSheet.Cells[4, 5] as Excel.Range).Value;
            konst.functionRampTime = (double)(xlSheet.Cells[5, 5] as Excel.Range).Value;
            konst.functionMagnitude = (double)(xlSheet.Cells[6, 5] as Excel.Range).Value;
            konst.functionBaseValue = (double)(xlSheet.Cells[7, 5] as Excel.Range).Value;
            konst.speedNoLoad = (double)(xlSheet.Cells[8, 5] as Excel.Range).Value;
            konst.stallTorque = (double)(xlSheet.Cells[9, 5] as Excel.Range).Value;
            konst.inertiaRotor = (double)(xlSheet.Cells[10, 5] as Excel.Range).Value;
            konst.inertiaExtra = (double)(xlSheet.Cells[11, 5] as Excel.Range).Value;
            konst.mechanicalTimeConstant = (double)(xlSheet.Cells[12, 5] as Excel.Range).Value;
            konst.noLoadCurrent = (double)(xlSheet.Cells[13, 5] as Excel.Range).Value;
            konst.torqueConstant = (double)(xlSheet.Cells[14, 5] as Excel.Range).Value;
            konst.motorViscousFrictionConstant = (double)(xlSheet.Cells[15, 5] as Excel.Range).Value;
            konst.inertiaTotal = (double)(xlSheet.Cells[16, 5] as Excel.Range).Value;
            konst.terminalInductance = (double)(xlSheet.Cells[17, 5] as Excel.Range).Value;
            konst.terminalResistance = (double)(xlSheet.Cells[18, 5] as Excel.Range).Value;
            konst.dataSheetVoltage = (double)(xlSheet.Cells[19, 5] as Excel.Range).Value;
            konst.stallCurrent = (double)(xlSheet.Cells[20, 5] as Excel.Range).Value;
            konst.pi = Math.PI;
            konst.timeIncrement = (double)(xlSheet.Cells[21, 5] as Excel.Range).Value;
            konst.timeSpan = (double)(xlSheet.Cells[22, 5] as Excel.Range).Value;

            workbook.Close(false, Type.Missing, Type.Missing);
            excel_app.Quit();
            constantsLoaded = true;
            constantsRead = true;
            appLoadPanel.Refresh();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //from: http://www.csharphelper.com/howtos/howto_read_excel.html
            loadConstants(ref (konst));
            //appLoad.Visible = true;
            //just need sender Panel
            //System.Windows.Forms.PaintEventArgs eFabricated;

            //eFabricated.Graphics = null;
            //appLoadPanel_Paint(appLoadPanel,    eFabricated);
            //appLoadPanel.Paint += new PaintEventHandler(appLoadPanel_Paint);
            //appLoadPanel.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void simpleNextState(ref double timeNow, ref double currentNow, ref double speedNow, 
            ref double positionNow, konstants konst)
        {
            double appLoadNow = appliedLoad(timeNow, konst);
            double rtrSpeed = rotorTopSpeedAdjusted(appLoadNow, konst);
            speedNow = speedNow + (rtrSpeed - speedNow) * (1 -
                Math.Exp(-konst.timeIncrement
                / konst.mechanicalTimeConstant * konst.inertiaRotor / konst.inertiaTotal)); ;
            //above is wrong! maybe fixed

            currentNow = (konst.stallCurrent - konst.noLoadCurrent) * (1 - speedNow / konst.speedNoLoad) +
                konst.noLoadCurrent;
            //Integrate[A+(B-A)*e^(-t*C),{t,0,D}]
            //A speedNow
            //B rtrSpeed
            //C konst.inertiaRotor/( konst.mechanicalTimeConstant *konst.inertiaTotal);
            //D timeIncrement
            //(e^(-C D) (A - B + (B + A (-1 + C D)) e^(C D)))/C
            Double A = speedNow;
            Double B = rtrSpeed;
            Double C = konst.inertiaRotor/(konst.mechanicalTimeConstant*konst.inertiaTotal);
            Double D = konst.timeIncrement;

            positionNow = positionNow + (Math.Exp(-C*D) *(A-B+(B+A*(-1+C*D))*Math.Exp(C*D) ))/C ;

            timeNow = timeNow + konst.timeIncrement;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //makeMotorRun
            mMRinWithRK4.Hide();
            button3.Hide();
            if (outputToExcel.Checked == false)
                outputToExcel.Hide();
            if (constantsLoaded == false)
                loadConstants(ref (konst));
            double timeNow = 0;
            double positionNow = float.Parse( revolutionInit.Text);
            double speedNow = float.Parse(rpsInit.Text);
            double currentNow = float.Parse(ampInit.Text);
            dataPoint thePoint;
            long trialArraySize = Convert.ToInt64( Math.Floor((konst.timeSpan / konst.timeIncrement)+1));
            long trialIndex = 0;
            //dataPoint[] dataPointArray = new dataPoint[dataPointslList.Count];
            dataPoint[] trialArray = new dataPoint[trialArraySize];
            if (linkedListCreated == false)
            {
                thePoint.current = currentNow;
                thePoint.speed = speedNow;
                thePoint.position = positionNow;
                thePoint.time = timeNow;
                dataPointslList.AddLast(thePoint);
                trialArray[trialIndex] = thePoint;  
                trialIndex++;
                linkedListCreated = true;
                while (timeNow < konst.timeSpan-konst.timeIncrement)
                {
                    simpleNextState(ref timeNow, ref currentNow, ref speedNow, ref positionNow, konst);
                    revolutionInit.Text = Convert.ToString(positionNow);
                    rpsInit.Text = Convert.ToString(speedNow);
                    ampInit.Text = Convert.ToString(currentNow);
                    thePoint.time = timeNow;
                    thePoint.position = positionNow;
                    thePoint.speed = speedNow;
                    thePoint.current = currentNow;
                    dataPointslList.AddLast(thePoint);
                    trialArray[trialIndex] = thePoint;
                    trialIndex++;

                    if (positionNow > maxRevolution)
                        maxRevolution= positionNow;
                }
                linkedListCreated = true;
                revolutionVSTme.Refresh();
                rps.Refresh();
                amp.Refresh();
                revolutionVSTme.Refresh();
            } 
            exportDataToExcel();
            
            
        }
        public void exportDataToExcel ()
        {
            /*Excel.Application excel_app = new Excel.ApplicationClass();
            excel_app.Visible = true;
            //Excel.Workbook workbook = excel_app.Workbooks.Open(xlFileName);
            Excel.Workbook workbook = excel_app.Workbooks.Item;
            Excel.Worksheet xlSheet = (Excel.Worksheet)workbook.Sheets[1];

            workbook.Close(false, Type.Missing, Type.Missing);
            excel_app.Quit();*/
            //dataPoint[] dataPointArray = new dataPoint[1];
            //dataPoint[] dataPointArray = new dataPoint[Convert.ToInt64(konst.timeSpan / konst.timeIncrement + 1)];
            long dataPointIndex = 1;
            if ((linkedListCreated == true) & (outputToExcel.Checked==true))
            {
               //dataPointsListCopy = dataPointslList;

                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }
                xlApp.Visible = false;
                //xlApp.Calculation = Excel.XlCalculation.xlCalculationManual;
                //xlApp.Calculation = XlCalculation.xlCalculationManual;
                //xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual;
                //ExcelApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual;  
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                System.Reflection.Missing misValue;
                misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlApp.Calculation = Excel.XlCalculation.xlCalculationManual;
                
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 1] = "time";
                xlWorkSheet.Cells[1, 2] = "position";
                xlWorkSheet.Cells[1, 3] = "speed";
                xlWorkSheet.Cells[1, 4] = "current";
                long rowValue = 2;
                dataPoint anElement;
                anElement.time = (trialArray[0].time);
                anElement.position = (trialArray[0].position);
                anElement.speed = (trialArray[0].speed);
                anElement.current = (trialArray[0].current);
                while (dataPointIndex<=(Convert.ToInt64(konst.timeSpan/konst.timeIncrement-2)))
                {
                    anElement = trialArray[dataPointIndex];
                    xlWorkSheet.Cells[rowValue, 1] = anElement.time;
                    xlWorkSheet.Cells[rowValue, 2] = anElement.position;
                    xlWorkSheet.Cells[rowValue, 3] = anElement.speed;
                    xlWorkSheet.Cells[rowValue, 4] = anElement.current;

                    dataPointIndex++;
                    rowValue = rowValue + 1;
                    rowsTextBox.Text = rowValue.ToString();
                }
                //xlApp.Calculation = XlCalculation.xlCalculationAutomatic;
                //xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic;
                xlApp.Visible = true;
                //dataPointslList = dataPointsListCopy;
                linkedListCreated = false;
                xlApp.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
            }
            //revolutionVSTme.Refresh();
            
        }

        private void revolutionInit_TextChanged(object sender, EventArgs e)
        {
            float floatResult;
            try
            { floatResult = float.Parse(revolutionInit.Text);
              
            }
            catch
            { revolutionInit.Text = "7"; }
            positionNow = float.Parse(revolutionInit.Text);
        }

        private void rpsInit_TextChanged(object sender, EventArgs e)
        {
            float floatResult;
            try
            { floatResult = float.Parse(rpsInit.Text); }
            catch
            { rpsInit.Text = "7"; }
            velocityNow = float.Parse(rpsInit.Text);
        }

        private void ampInit_TextChanged(object sender, EventArgs e)
        {
            float floatResult;
            try
            { floatResult = float.Parse(ampInit.Text); }
            catch
            {
                ampInit.Text = "7";
            }
            currentNow = float.Parse(ampInit.Text);
        }
        private double appliedLoad(double inputTime, konstants konst)
        { double revisedTime;
            double returnValue;
            revisedTime = (inputTime / konst.functionPeriod - 
                Math.Floor(inputTime / konst.functionPeriod))*konst.functionPeriod;
            if (revisedTime <= konst.functionPeriod / 2)
            {
                returnValue = 0;
            }
            else
            {
                if (revisedTime < konst.functionPeriod / 2 + konst.functionRampTime)
                {
                    returnValue = konst.functionMagnitude*(revisedTime - konst.functionPeriod/2) /
                        konst.functionRampTime ;
                }
                else
                {
                    if (revisedTime > konst.functionPeriod - konst.functionRampTime)
                    {
                        returnValue = (konst.functionPeriod-revisedTime)/konst.functionRampTime*konst.functionMagnitude;
                    }
                    else
                    {
                        returnValue = konst.functionMagnitude;
                    }
                }
            }
            returnValue = returnValue + konst.functionBaseValue;
            return returnValue;
            
        }

        private void appLoadPanel_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;
            
            /*g.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.ForestGreen)), p.DisplayRectangle);
            System.Drawing.Point[] points = new System.Drawing.Point[4];
            points[0] = new System.Drawing.Point(10, 10);
            points[1] = new System.Drawing.Point(10, p.Height);
            points[2] = new System.Drawing.Point(p.Width, p.Height);
            points[3] = new System.Drawing.Point(p.Width, 10);
            Brush brush = new SolidBrush(Color.Azure);*/
            Pen myPen = new Pen(System.Drawing.Color.Red,4);
            //Font myFont = new Font("Helctiva",10,FontStyle.Bold);
            Brush myBrush = new SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Font myFont = new System.Drawing.Font("Helectiva",10,FontStyle.Bold);
            //appLoadPanel.Refresh();
            System.Drawing.Point myPoint = new System.Drawing.Point(10, 10);
            if (constantsLoaded)
            {
                System.Drawing.Point[] points = new System.Drawing.Point[6];
                double xDouble;
                double yDouble;
                double timeValue;
                double functionValue;
                System.Drawing.Point pointToDraw (double timeVal)
                {
                    
                    functionValue = appliedLoad(timeVal, konst);
                    xDouble = timeVal / konst.functionPeriod * p.Width;
                    yDouble = p.Height / 2 - functionValue / konst.stallTorque * (p.Height / 2);
                    System.Drawing.Point returnValue = new System.Drawing.Point(Convert.ToInt32(xDouble),Convert.ToInt32(yDouble));
                    return returnValue;
                }

                
                points[0] = pointToDraw(0);
                points[1] = pointToDraw(0+konst.functionPeriod/2);
                points[2] = pointToDraw(konst.functionPeriod / 2 + konst.functionRampTime);
                points[3] = pointToDraw(0 + konst.functionPeriod - konst.functionRampTime);
                points[4] = pointToDraw(0 + konst.functionPeriod - konst.functionRampTime / 2);
                points[5] = pointToDraw(0 + konst.functionPeriod);
                
                g.DrawLine(myPen, points[0], points[1]);
                g.DrawLine(myPen , points[1], points[2]);
                g.DrawLine(myPen, points[2], points[3]);
                g.DrawLine(myPen, points[3], points[4]);
                g.DrawLine(myPen, points[4], points[5]);


            }
                
                
        }
        private double rotorTopSpeedAdjusted(double rotorLoad, konstants konst)
        {
            double returnValue;
            returnValue = konst.speedNoLoad * (1 - rotorLoad / konst.stallTorque);
            return returnValue;
        }

        private void revolutionVSTme_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;
            float penSize = 2.0F;
            dataPoint singlePoint;
            long trialArraySize;
            long trialIndex = 0;
            int dataPointIndex = 0;
            if (constantsLoaded == true)
            { trialArraySize = Convert.ToInt64(Math.Floor((konst.timeSpan / konst.timeIncrement) + 1)); }

            //dataPoint[] dataPointArray = new dataPoint[trialArraySize];
            dataPoint[] dataPointArray = new dataPoint[1];
            //dataPoint[] dataPointArray = new dataPoint[dataPointslList.Count];
            if (constantsLoaded == true)
            {
                dataPointArray = new dataPoint[Convert.ToInt64(konst.timeSpan / konst.timeIncrement + 1)];
            }

            if ((linkedListCreated == true) ) //& (constantsLoaded == true) 
            {
                Pen myPen = new Pen(System.Drawing.Color.Red, penSize);
                Brush myBrush = new SolidBrush(System.Drawing.Color.Azure);
                System.Drawing.Rectangle aRect;
                aRect = new System.Drawing.Rectangle();
                //aRect.Height = 1;
                //aRect.Width = 1;

                dataPointslList.CopyTo(dataPointArray, 0);
                singlePoint = dataPointArray[0];
                //aRect.X = Convert.ToInt32(singlePoint.time / dataPointslList.Last.Value.time * p.Width);
                  aRect.X = Convert.ToInt32(singlePoint.time / dataPointslList.Last.Value.time * p.Width);
                //aRect.X = Convert.ToInt32(singlePoint.time / dataPointArray[dataPointArray.Length-7].time * p.Width);
                aRect.Y = Convert.ToInt32(p.Height-singlePoint.position/maxRevolution*p.Height);
                aRect.Width = 1;
                aRect.Height=1;
                g.FillEllipse(myBrush, aRect);
                g.DrawEllipse(myPen, aRect);
                //dataPointslList.RemoveFirst();

                dataPointIndex = dataPointIndex + 1;
                //singlePoint = dataPointArray[dataPointIndex];
                singlePoint = dataPointArray[dataPointIndex];
                while (dataPointIndex < dataPointslList.Count - 1)
                {
                    aRect.X = Convert.ToInt32(singlePoint.time / dataPointslList.Last.Value.time * p.Width);
                    aRect.Y = Convert.ToInt32(p.Height - singlePoint.position / maxRevolution * p.Height);
                    g.FillEllipse(myBrush, aRect);
                    g.DrawEllipse(myPen, aRect);
                    dataPointIndex = dataPointIndex + 1;
                    singlePoint = dataPointArray[dataPointIndex];
                }
                //dataPointslList = dataPointsListCopy;
                trialArray = dataPointArray;
                linkedListCreated = true;
              
            }   
        }

        private void rps_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;
            float penSize = 2.0F;
            dataPoint singlePoint;
            dataPoint[] dataPointArray = new dataPoint[1];
            //dataPoint[] dataPointArray = new dataPoint[dataPointslList.Count];
            if (constantsLoaded == true)
            {
                dataPointArray = new dataPoint[Convert.ToInt64(konst.timeSpan / konst.timeIncrement + 1)];
            }
            int dataPointIndex = 0;
            
            if (linkedListCreated == true)
            {
                Pen myPen = new Pen(System.Drawing.Color.Red, penSize);
                Brush myBrush = new SolidBrush(System.Drawing.Color.Azure);
                System.Drawing.Rectangle aRect;
                aRect = new System.Drawing.Rectangle();
               
                dataPointslList.CopyTo(dataPointArray,0);
                singlePoint = dataPointArray[0];

                aRect.X = Convert.ToInt32(singlePoint.time / dataPointslList.Last.Value.time * p.Width);
                aRect.Y = Convert.ToInt32(p.Height - singlePoint.speed / konst.speedNoLoad * p.Height);
                aRect.Width = 1;
                aRect.Height = 1;
                g.FillEllipse(myBrush, aRect);
                g.DrawEllipse(myPen, aRect);
                //dataPointslList.RemoveFirst();
                dataPointIndex = dataPointIndex + 1;
                singlePoint = dataPointArray[dataPointIndex];

                while (dataPointIndex<dataPointslList.Count-1)
                {
                    aRect.X = Convert.ToInt32(singlePoint.time / dataPointslList.Last.Value.time * p.Width);
                    aRect.Y = Convert.ToInt32(p.Height - singlePoint.speed / konst.speedNoLoad * p.Height);
                    g.FillEllipse(myBrush, aRect);
                    g.DrawEllipse(myPen, aRect);
                    //dataPointslList.RemoveFirst();
                    //singlePoint = dataPointslList.First.Value;
                    dataPointIndex = dataPointIndex + 1;               
                    singlePoint = dataPointArray[dataPointIndex];
                }
                //dataPointslList = dataPointsListCopy;
                linkedListCreated = true;
            }
        }

        private void amp_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;
            float penSize = 2.0F;
            dataPoint singlePoint;
            dataPoint[] dataPointArray = new dataPoint[1];
            //dataPoint[] dataPointArray = new dataPoint[dataPointslList.Count];
            if (constantsLoaded == true)
            {
                dataPointArray = new dataPoint[Convert.ToInt64(konst.timeSpan / konst.timeIncrement + 1)];
            }
            int dataPointIndex = 0;
            if (linkedListCreated == true)
            {
                Pen myPen = new Pen(System.Drawing.Color.Red, penSize);
                Brush myBrush = new SolidBrush(System.Drawing.Color.Azure);
                System.Drawing.Rectangle aRect;
                aRect = new System.Drawing.Rectangle();

                dataPointslList.CopyTo(dataPointArray, 0);
                singlePoint = dataPointArray[0];

                aRect.X = Convert.ToInt32(singlePoint.time / dataPointslList.Last.Value.time * p.Width);
                aRect.Y = Convert.ToInt32(p.Height - singlePoint.current / konst.stallCurrent * p.Height);
                aRect.Width = 1;
                aRect.Height = 1;
                g.FillEllipse(myBrush, aRect);
                g.DrawEllipse(myPen, aRect);
                //dataPointslList.RemoveFirst();
                dataPointIndex = dataPointIndex + 1;
                singlePoint = dataPointArray[dataPointIndex];

                while (dataPointIndex < dataPointslList.Count - 1)
                {
                    aRect.X = Convert.ToInt32(singlePoint.time / dataPointslList.Last.Value.time * p.Width);
                    aRect.Y = Convert.ToInt32(p.Height - singlePoint.current / konst.stallCurrent * p.Height);
                    g.FillEllipse(myBrush, aRect);
                    g.DrawEllipse(myPen, aRect);
                    //dataPointslList.RemoveFirst();
                    //singlePoint = dataPointslList.First.Value;
                    dataPointIndex = dataPointIndex + 1;
                    singlePoint = dataPointArray[dataPointIndex];
                }
                //dataPointslList = dataPointsListCopy;
                linkedListCreated = true;
            }
        }
        double variableVoltage(double time,double speed, double Current, double postion)
        {
            return 24.0;
        }
        double effHundred(double timeNow, double speed, double Current, double position, konstants konst)
        {
            double result;
            result = -konst.motorViscousFrictionConstant / konst.inertiaTotal * speed;
            result = result + konst.torqueConstant / konst.inertiaTotal * Current;
            result = result - appliedLoad(timeNow, konst) / konst.inertiaTotal;

            return result;
        }
        double geeHundred(double timeNow, double speed , double Current, double position, konstants konst)
        {
            double result;
            result = -konst.torqueConstant / konst.terminalInductance * speed;
            result = result - konst.terminalResistance / konst.terminalInductance * Current;
            result = result + variableVoltage(timeNow, position, speed, Current) / konst.terminalInductance;
            return result;
        }
        double ehchHundred(double timeNow , double speed, double Current, double position , konstants konst)
        {
            double result;
            result = speed;
            return result;
        }
        private void simpleNextStateRK4(ref double timeNow, ref double currentNow, ref double speedNow,
            ref double positionNow, konstants konst)
        {
            //needs to written!
            Double[] kn = new double[5];
            Double[] ln = new double[5];
            Double[] mn = new double[5];
            speedNow = speedNow * 2 * konst.pi;
            positionNow = positionNow * 2 * konst.pi;
            kn[1] = effHundred(timeNow, speedNow, currentNow, positionNow, konst);
            double kkn = kn[1];
            ln[1] = geeHundred(timeNow, speedNow, currentNow, positionNow, konst);
            double lln = ln[1];
            mn[1] = ehchHundred(timeNow, speedNow, currentNow, positionNow, konst);
            double mmn = mn[1];
            kn[2] = effHundred(timeNow + konst.timeIncrement / 2, speedNow + konst.timeIncrement / 2 * kn[1], currentNow + konst.timeIncrement / 2 * ln[1], positionNow + konst.timeIncrement / 2 * mn[1], konst);
            ln[2] = geeHundred(timeNow + konst.timeIncrement / 2, speedNow + konst.timeIncrement / 2 * kn[1], currentNow + konst.timeIncrement / 2 * ln[1], positionNow + konst.timeIncrement / 2 * mn[1], konst);
            mn[2] = ehchHundred(timeNow + konst.timeIncrement / 2, speedNow + konst.timeIncrement / 2 * kn[1], currentNow + konst.timeIncrement / 2 * ln[1], positionNow + konst.timeIncrement / 2 * mn[1], konst);
            kkn = kn[2];
            lln = ln[2];
            mmn = mn[2];
            kn[3] = effHundred(timeNow + konst.timeIncrement / 2, speedNow + konst.timeIncrement / 2 * kn[2], currentNow + konst.timeIncrement / 2 * ln[2], positionNow + konst.timeIncrement / 2 * mn[2], konst);
            ln[3] = geeHundred(timeNow + konst.timeIncrement / 2, speedNow + konst.timeIncrement / 2 * kn[2], currentNow + konst.timeIncrement / 2 * ln[2], positionNow + konst.timeIncrement / 2 * mn[2], konst);
            mn[3] = ehchHundred(timeNow + konst.timeIncrement / 2, speedNow + konst.timeIncrement / 2 * kn[2], currentNow + konst.timeIncrement / 2 * ln[2], positionNow + konst.timeIncrement / 2 * mn[2], konst);
            kkn = kn[3];
            lln = ln[3];
            mmn = mn[3];
            kn[4] = effHundred(timeNow + konst.timeIncrement, speedNow + konst.timeIncrement * kn[3], currentNow + konst.timeIncrement * ln[3], positionNow + konst.timeIncrement * mn[3], konst);
            ln[4] = geeHundred(timeNow + konst.timeIncrement, speedNow + konst.timeIncrement * kn[3], currentNow + konst.timeIncrement * ln[3], positionNow + konst.timeIncrement * mn[3], konst);
            mn[4] = ehchHundred(timeNow + konst.timeIncrement, speedNow + konst.timeIncrement * kn[3], currentNow + konst.timeIncrement * ln[3], positionNow + konst.timeIncrement * mn[3], konst);
            kkn = kn[4];
            lln = ln[4];
            mmn = mn[4];
            speedNow = (speedNow + konst.timeIncrement / 6 * (kn[1] + 2 * kn[2] + 2 * kn[3] + kn[4]))/(2*Math.PI);
            currentNow = currentNow + konst.timeIncrement / 6 * (ln[1] + 2 * ln[2] + 2 * ln[3] + ln[4]);
            positionNow = (positionNow + konst.timeIncrement / 6 * (mn[1] + 2 * mn[2] + 2 * mn[3] + mn[4])) / (2 * Math.PI);
            timeNow = timeNow + konst.timeIncrement;
        }

        private void mMRinWithRK4_Click(object sender, EventArgs e)
        {
            button3.Hide();
            mMRinWithRK4.Hide();
            if (outputToExcel.Checked == false)
                outputToExcel.Hide();
            if (constantsLoaded == false)
                loadConstants(ref (konst));
            double timeNow = 0;
            double positionNow = float.Parse(revolutionInit.Text);
            double speedNow = float.Parse(rpsInit.Text);
            double currentNow = float.Parse(ampInit.Text);
            dataPoint thePoint;
            long trialArraySize = Convert.ToInt64(Math.Floor((konst.timeSpan / konst.timeIncrement) + 1));
            long trialIndex = 0;

            if (linkedListCreated == false)
            {
                thePoint.current = currentNow;
                thePoint.speed = speedNow;
                thePoint.position = positionNow;
                thePoint.time = timeNow;
                dataPointslList.AddLast(thePoint);
                dataPoint[] trialArray = new dataPoint[trialArraySize];
                linkedListCreated = true;
                trialArray[trialIndex] = thePoint;
                trialIndex++;
                while (timeNow < konst.timeSpan-konst.timeIncrement)
                {
                    simpleNextStateRK4(ref timeNow, ref currentNow, ref speedNow, ref positionNow, konst);
                    revolutionInit.Text = Convert.ToString(positionNow);
                    rpsInit.Text = Convert.ToString(speedNow);
                    ampInit.Text = Convert.ToString(currentNow);
                    thePoint.time = timeNow;
                    thePoint.position = positionNow;
                    thePoint.speed = speedNow;
                    thePoint.current = currentNow;
                    dataPointslList.AddLast(thePoint);
                    trialArray[trialIndex] = thePoint;
                    trialIndex++;
                    if (positionNow > maxRevolution)
                        maxRevolution = positionNow;
                }
                linkedListCreated = true;
                revolutionVSTme.Refresh();
                rps.Refresh();
                amp.Refresh();
                revolutionVSTme.Refresh();
            }
            exportDataToExcel();
        }
    }
}
