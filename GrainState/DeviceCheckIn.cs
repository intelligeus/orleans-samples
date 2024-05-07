namespace GrainState;

/* Keep track of the last check in times of a Telematic Device
 
    Normally we would also track vehicle id and last GPS location etc here
*/
public class DeviceCheckIn
{
    public string DeviceId { get; set; }
    public DateTime LastCheckInTime { get; set; }

}