// See https://aka.ms/new-console-template for more information
var packetPairs = File.ReadAllLines("input.txt").Chunk(3);

List<int> inOrder = new List<int>();
int index = 0;
foreach (var pair in packetPairs)
{
    string packetString1 = pair[0];
    string packetString2 = pair[1];
    Packet packetLeft = Packet.ParseString(packetString1);
    Packet packetRight = Packet.ParseString(packetString2);
    Console.WriteLine(packetLeft.ToString());
    Console.WriteLine(packetRight.ToString());
    if (packetLeft.ComparePackets(packetRight) < 0)
    {
        inOrder.Add(index + 1);
    }
    index++;

}

int sumOfInOrder = inOrder.Sum();
Console.WriteLine(sumOfInOrder);
Console.WriteLine("Done");

public class Packet
{
    public const string DIGITS = "0123456789";
    public List<Packet> Packets {get; set;} = new List<Packet>();
    public int? PacketValue {get; set;}
    // 1 if bigger
    // -1 if smaller
    // 0 if equal
    public int ComparePackets(Packet other)
    {
        if (PacketValue.HasValue && other.PacketValue.HasValue)
        {
            return PacketValue.Value.CompareTo(other.PacketValue.Value);
        }
        else if (PacketValue.HasValue && !other.PacketValue.HasValue)
        {
            Packet wrappedPacket = new Packet();
            wrappedPacket.Packets.Add(this);
            return wrappedPacket.ComparePackets(other);
        }
        else if (!PacketValue.HasValue && other.PacketValue.HasValue)
        {
            Packet wrappedPacket = new Packet ();
            wrappedPacket.Packets.Add(other);
            return ComparePackets(wrappedPacket);
        }
        else
        {
            int thisLength = Packets.Count;
            int thatLength = other.Packets.Count;
            int shorter = Math.Min(thisLength, thatLength);
            for (int i = 0; i < shorter; i++)
            {
                int cmpResult = Packets[i].ComparePackets(other.Packets[i]);
                if (cmpResult != 0)
                {
                    return cmpResult;
                }
            }
            return thisLength.CompareTo(thatLength);
        }
    }

    public override string ToString()
    {
        if (PacketValue.HasValue)
        {
            return $"{PacketValue.Value}";
        }
        else
        {
            return $"[{string.Join(",", Packets.Select(p => p.ToString()))}]";
        }
    }

    public static Packet ParseString(string packetString)
    {
        Packet rootPacket = null;
        Packet currentPacket = null;
        Stack<Packet> packetStack  = new Stack<Packet>();
        for (int i = 0; i < packetString.Length; i++)
        {
            char packetChar = packetString[i];
            if (packetChar == '[')
            {
                Packet newPacket = new Packet();
                packetStack.Push(newPacket);
                if (currentPacket != null) 
                {
                    currentPacket.Packets.Add(newPacket);
                }
                currentPacket = packetStack.Peek();
            }
            else if (packetChar == ']')
            {
                Packet p = packetStack.Pop();
                if (packetStack.Count == 0)
                {
                    rootPacket = p;
                }
                else
                {
                    currentPacket = packetStack.Peek();
                }
            }
            else if (packetChar == ',')
            {
                continue;
            }
            else
            {
                string valString = packetChar.ToString();
                while (char.IsNumber(packetString[i+1]))
                {
                    valString += packetString[i+1];
                    i++;
                }
                int val = Convert.ToInt32(valString);
                Packet packetLeaf = new Packet {PacketValue = val};
                currentPacket.Packets.Add(packetLeaf);
            }
        }
        return rootPacket;
    }
}

