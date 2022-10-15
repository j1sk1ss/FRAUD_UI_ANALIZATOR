﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FRAUD_UI_ANALIZATOR.SCRIPTS;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using OfficeOpenXml;
using Path = System.IO.Path;
namespace FRAUD_UI_ANALIZATOR
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private readonly JsonParser _jsonParser = new();
        private Dictionary<string, TransactiondData> _transactionsData = new();
        private void LoadJson(object sender, RoutedEventArgs e)
        {
            try
            { var childhood = new OpenFileDialog
                { Filter = "JSON Files (*.json)|*.json",
                    FilterIndex = 1,
                    Multiselect = true };
                if (childhood.ShowDialog() != true) return;
                var fileName = childhood.FileName;
                DbPathLabel.Content = Path.GetFileName(fileName);
                _transactionsData = _jsonParser.StartParse(fileName); }
            catch (Exception exception) {
                DbPathLabel.Content = "";
                MessageBox.Show($"Error with: {exception}", "Error with Parsing!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void PatternGet(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_transactionsData.Count < 1)
            {
                MessageBox.Show("Load Json before start!", "Pattern getting error!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            var folderBrowser = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Report"
            };
                var directoryName = string.Empty;
                    if (folderBrowser.ShowDialog() == true) {
                        directoryName = Path.GetDirectoryName(folderBrowser.FileName); }

                    try
                    {
                        var lst = ExelConstructor.InitList(_transactionsData, _jsonParser.KeyList);
                        PatternInit(lst);
                        using var excelPackage = new ExcelPackage();
                        ExelConstructor.ExcelWrite(excelPackage, _transactionsData, lst);
                        excelPackage.SaveAs(directoryName + @"\Report.xlsx");
                        OpenChartsBut.Visibility = Visibility.Visible;
                        Chart.Series.Clear();
                        foreach (var t in lst)
                            Chart.Series.Add(new PieSeries
                            {
                                Title = $"{t.Split(" ")[0]}",
                                Values = new ChartValues<int> { t.Split(" ").Length - 1}
                            });

                        DataContext = this;
                    }
                catch (Exception e)
                { MessageBox.Show($"Error with: {e}", "Error with analysing!", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
        }

        private const string path = "pack://application:,,,";
        private void StrangeTimeCheck(object sender, RoutedEventArgs routedEventArgs)
        {
            StrangeTime.Source = StrangeTime.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void SmallTrn(object sender, RoutedEventArgs routedEventArgs)
        {
            SmallTransaction.Source = SmallTransaction.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void BigTrn(object sender, RoutedEventArgs routedEventArgs)
        {
            BigTransaction.Source = BigTransaction.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void NotValidPas(object sender, RoutedEventArgs routedEventArgs)
        {
            NotValidPassport.Source = NotValidPassport.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void NotValidAcc(object sender, RoutedEventArgs routedEventArgs)
        {
            NotValidAccount.Source = NotValidAccount.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void Duration(object sender, RoutedEventArgs routedEventArgs)
        {
            DurationPattern.Source = DurationPattern.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void DifCities(object sender, RoutedEventArgs routedEventArgs)
        {
            DifferentCities.Source = DifferentCities.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void ManyCards(object sender, RoutedEventArgs routedEventArgs)
        {
            TooManyCards.Source = TooManyCards.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void ManyPos(object sender, RoutedEventArgs routedEventArgs)
        {
            TooManyPos.Source = TooManyPos.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void ManyPassports(object sender, RoutedEventArgs routedEventArgs)
        {
            TooManyPassports.Source = TooManyPassports.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void OlderPattern(object sender, RoutedEventArgs routedEventArgs)
        {
            Older.Source = Older.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void Cancelled(object sender, RoutedEventArgs routedEventArgs)
        {
            CancelledStreak.Source = CancelledStreak.Source.ToString() == $"{path}/IMG/patterninactive_button.png" ?
                new BitmapImage(new Uri(@"/IMG/pattern_button.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/patterninactive_button.png", UriKind.Relative));
        }
        private void TransactionType(object sender, RoutedEventArgs routedEventArgs)
        {
            Type.Source = Type.Source.ToString() == $"{path}/IMG/unchecked_checkbox.png" ?
                new BitmapImage(new Uri(@"/IMG/checked_checkbox_1.png", UriKind.Relative)) : 
                new BitmapImage(new Uri(@"/IMG/unchecked_checkbox.png", UriKind.Relative));
        }
        private void OpenCharts(object sender, RoutedEventArgs routedEventArgs)
        {
            Charts.Visibility = Visibility.Visible;
        }
        private void OpenMenu(object sender, RoutedEventArgs routedEventArgs)
        {
            Charts.Visibility = Visibility.Hidden;
        }
        [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH", MessageId = "type: System.String")]
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.String")]
        private void PatternInit(ICollection<string> lst)
        {
            try
            { 
                if (StrangeTime.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("GTP " + PatternGetter.GetTimePattern(_transactionsData, _jsonParser.KeyList, 
                        TimeSpan.Parse(EndTimeTp.Text), TimeSpan.Parse(StartTimeTp.Text)));
                if (SmallTransaction.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("SAP " + PatternGetter.GetSmallAmountPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(SmallAmount.Text)));
                if (BigTransaction.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("BAP " + PatternGetter.GetBigAmountPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(BigAmount.Text)));
                if (NotValidPassport.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("PVP " + PatternGetter.GetPassportValidPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(PassportDays.Text)));
                if (NotValidAccount.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("AVP " + PatternGetter.GetAccountValidPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(AccountDays.Text)));
                if (DurationPattern.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("TDP " + PatternGetter.GetTimeDurationPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(DurationStreak.Text), TimeSpan.Parse(DurationInterval.Text)));
                if (DifferentCities.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("DCP " + PatternGetter.GetDifferentCityPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(CitiesCount.Text)));
                if (TooManyCards.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("MCP " + PatternGetter.GetMultiCardPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(CardCount.Text)));
                if (TooManyPos.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("MPP " + PatternGetter.GetMultiPosPatter(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(PosCount.Text)));
                if (TooManyPassports.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("MPC " + PatternGetter.GetMultiPassportCard(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(PassportCount.Text)));
                if (Older.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("GOP " + PatternGetter.GetOldersPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(Age.Text), Type.Source.ToString() == $"{path}/IMG/unchecked_checkbox.png"));
                if (CancelledStreak.Source.ToString() == $"{path}/IMG/pattern_button.png") 
                    lst.Add("GCP " + PatternGetter.GetCancelledPattern(_transactionsData, _jsonParser.KeyList, 
                        int.Parse(StreakCount.Text)));
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error with: {e}", "Pattern getting error!", MessageBoxButton.OK);
            }
        }
    }
}