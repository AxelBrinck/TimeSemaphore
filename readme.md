# TimeSemaphore

## Description
This is a small tool used to limit the calls to an API to avoid banning.

## Usage

You can use **Wait()** to make your thread to wait until enough room for an additional execution.

You can use **IsGreen()** to peek.

`var semaphore = new TimeSemaphore(1200, TimeSpan.FromSeconds(60));`

Here we are basically saying that we want a maximum traffic of 1200 calls in the last 60 seconds.