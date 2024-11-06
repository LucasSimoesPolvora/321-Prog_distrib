using System;
using System.Net;
using System.Net.Sockets;


Main();
void Main()
{
    string ntpServer = "0.ch.pool.ntp.org";

    byte[] ntpData = new byte[48];
    ntpData[0] = 0x1b;

    IPEndPoint ntpEndpoint = new IPEndPoint(Dns.GetHostAddresses(ntpServer)[0], 123);

    UdpClient ntpClient = new UdpClient();
    ntpClient.Connect(ntpEndpoint);

    ntpClient.Send(ntpData, ntpData.Length);

    ntpData = ntpClient.Receive(ref ntpEndpoint);

    NtpPacket ntpPacket = new NtpPacket();

    DateTime ntpTime = ntpPacket.ToDateTime(ntpData);

    Console.WriteLine("Heure actuelle : ");
    Console.WriteLine($"- {ntpTime.ToString("dddd, dd MMMM yyyy")}");
    Console.WriteLine($"- {ntpTime.ToString("dd.mm.yyyy HH:mm:ss")}");
    Console.WriteLine($"- {ntpTime.ToString("dd.mm.yyyy")}\n");

    TimeSpan timeSpan = DateTime.Now - ntpTime;

    Console.WriteLine($"Différence de temps : {timeSpan}");

    DateTime local = ntpTime + timeSpan;

    Console.WriteLine($"Heure actuelle corrigée : {local.ToString()}");

    Console.WriteLine($"Heure UTC : {local.ToUniversalTime().ToString()}");

    Console.WriteLine($"Heure locale : {ntpTime.ToLocalTime()}");

    Console.WriteLine($"Heure GMT: {local.ToUniversalTime().AddHours(-1)}");

    Console.WriteLine($"Heure Gmt en local: {local.ToUniversalTime().AddHours(-1).AddHours(1)}");

    ntpClient.Close();

    Console.ReadLine();
}

public class NtpPacket()
{
    public DateTime ToDateTime(byte[] ntpData)
    {
        ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
        ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

        var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
        var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);
        return networkDateTime;
    }
}