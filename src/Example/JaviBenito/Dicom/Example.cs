using System;
using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Network;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;

using VMS.TPS.Common.Model.API;

namespace Javibenito.DICOM
{

    public class Example
    {
        private const string LocalHost = "127.0.0.1";
        private const string RemoteAeTitle = "ORTHANC";
        private const string RemoteHost = "127.0.0.1";
        private const int RemotePort = 4242;
        private const string LocalAeTitle = "EVILSCU";
        private const int LocalPort = 11112;

        public static void Main()
        {

            var localEntity = new Entity(LocalAeTitle, LocalHost, LocalPort);
            var remoteEntity = new Entity(RemoteAeTitle, RemoteHost, RemotePort);
            var scu = new DICOMSCU(localEntity)
            {
                ConnectionTimeout = 5000,
                IdleTimeout = 5000
            };

            ushort messageId = 1;
            CEchoResponse echoResponse = scu.GetResponse<CEchoResponse, CEchoRequest>(new CEchoRequest(messageId), remoteEntity, ref messageId);
            bool echoSucceeded = echoResponse != null;

            Console.WriteLine($"C-ECHO to {RemoteAeTitle}@{RemoteHost}:{RemotePort}: {(echoSucceeded ? "OK" : "FAILED")}");
            if (echoResponse != null)
            {
                Console.WriteLine($"C-ECHO status: 0x{echoResponse.Status:X4}");
            }

            if (!echoSucceeded)
            {
                Console.WriteLine("Could not establish a DICOM association.");
                return;
            }

            var finder = scu.GetCFinder(remoteEntity);
            List<CFindPatientIOD> patients = finder.FindPatient().ToList();

            Console.WriteLine($"Patients returned by C-FIND: {patients.Count}");

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients returned from the DICOM query.");
                return;
            }

            CFindPatientIOD firstPatient = patients[0];
            string patientName = firstPatient.PatientsName?.Data ?? "(unknown)";
            string patientId = firstPatient.PatientId ?? "(unknown)";
            string patientSex = firstPatient.PatientSex ?? "(unknown)";

            Console.WriteLine($"Patient Name: {patientName}");
            Console.WriteLine($"Patient ID: {patientId}");
            Console.WriteLine($"Patient Sex: {patientSex}");

            List<CFindStudyIOD> studies = string.IsNullOrWhiteSpace(firstPatient.PatientId)
                ? new List<CFindStudyIOD>()
                : finder.FindStudies(firstPatient.PatientId).ToList();

            Console.WriteLine($"Studies for patient: {studies.Count}");

            if (studies.Count == 0)
            {
                return;
            }

            CFindStudyIOD firstStudy = studies[0];
            Console.WriteLine($"Study UID: {firstStudy.StudyInstanceUID ?? "(unknown)"}");
            Console.WriteLine($"Study ID: {firstStudy.StudyId ?? "(unknown)"}");

            List<CFindSeriesIOD> series = finder.FindSeries(firstStudy).ToList();
            Console.WriteLine($"Series in study: {series.Count}");

            if (series.Count == 0)
            {
                return;
            }

            CFindSeriesIOD firstSeries = series[0];
            Console.WriteLine($"Series UID: {firstSeries.SeriesInstanceUID ?? "(unknown)"}");
            Console.WriteLine($"Series Description: {firstSeries.SeriesDescription ?? "(unknown)"}");
            Console.WriteLine($"Modality: {firstSeries.Modality ?? "(unknown)"}");

            List<CFindInstanceIOD> instances = finder.FindInstances(firstSeries).ToList();
            Console.WriteLine($"Instances in first series: {instances.Count}");

            if (instances.Count == 0)
            {
                return;
            }

            CFindInstanceIOD firstInstance = instances[0];
            Console.WriteLine($"SOP Class UID: {firstInstance.SOPClassUID ?? "(unknown)"}");
            Console.WriteLine($"SOP Instance UID: {firstInstance.SOPInstanceUID ?? "(unknown)"}");

        }
    }
}

