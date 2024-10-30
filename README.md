## By runing following scripts you can clone the source code and run it on your linux system


`sudo apt update`

### Install git
`sudo apt install git`

### Update package index
`sudo apt update`

### Install required dependencies
`sudo apt install -y wget apt-transport-https`

### Download the Microsoft package signing key
`wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb`

### Update package index again
`sudo apt update`

### Install the .NET SDK
`sudo apt install -y dotnet-sdk-8.0`

### Verify the installation
`dotnet --version`

### Make directory to clone source code
`sudo mkdir Verivox`
`cd Verivox`

### Checkout source code
`sudo git clone https://github.com/mahahmadi360/Verivox.git`

### Redirect to end point project
`cd Verivox/src/EndPoints/Verivox.EnergyCostCalculator.WebApi`

### Publish end point
`sudo dotnet publish -c release -o ../../../publish`

### Redirect to publish folder
`cd ../../../publish`

### Run application
`dotnet Verivox.EnergyCostCalculator.WebApi.dll`

## After run, use following address to test the functionality

/api/getCosts/\{consumption:kwH\}