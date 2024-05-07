namespace CommonGrains;

/* Keep track of the last check in times of a Telematic Device
 
    Normally we would also track vehicle id and last GPS location etc here
*/
[Serializable]
public class DeviceCheckIn
{
    public string DeviceId { get; set; }
    public DateTime LastCheckInTime { get; set; }

}