using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybrid.Services;

internal class NativeService
{
    public void TriggerHapticFeedback()
    {
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
    }

    public async Task ToggleFlashlightAsync(bool turnon)
    {
        try
        {
            if (turnon)
                await Flashlight.Default.TurnOnAsync();
            else
                await Flashlight.Default.TurnOffAsync();
        }
        catch (FeatureNotSupportedException ex)
        {
            // Handle not supported on device exception
        }
        catch (PermissionException ex)
        {
            // Handle permission exception
        }
        catch (Exception ex)
        {
            // Unable to turn on/off flashlight
        }
    }

}
