namespace CommonGrains;

/* Keep track of the last check in times of a Telematic Device
 
    Normally we would also track vehicle id and last GPS location etc here
*/
[Serializable]
public class DeviceMessage : DeviceCheckIn 
{
    public string Message { get; set; }
    public string Location { get; set; }

}