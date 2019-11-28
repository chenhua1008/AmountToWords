# AmountToWords

### About this
This is a C# application to convert any amount to its English currency representation in words.  
Example:   
>Input - 1.15  
>Output - "One Dollar and Fifteen Cents"  

### Prerequisites
Windows OS with .Net framework 4.0 or higher 

### How to run
1. Download the "[AmountToWords.exe](https://github.com/chenhua1008/AmountToWords/tree/master/AmountToWords/bin/Release)" from "AmountToWords/bin/Release" to your computer.  
2. Double click "AmountToWords.exe" to open it.  
3. Input the amount you want to convert.  

![](https://github.com/chenhua1008/AmountToWords/blob/master/AmountToWordsConsole.png) 

### Assumption
* Input data range: 0 to 2147483647.99(integer part is the maximum of Int, and decimal part is the maximum of 2 digits). 
* Supported input data format: Any digital numbers with or without decimal part, e.g. '123', '123.4', '123.45', '0123.45' etc.  
* Can not support non-numeric input, e.g 'one dollar', '$3', '123,45' etc.    
(Above assumptions and limitations are based on my understanding and common sense. They should depend on actual business requirements in real work environment.)


### Solution structure
Solution 'AmountToWords'

>>>|-------- Project 'AmountToWords'

>>>>>>|---------source 'AmountToWords.cs'  
>>>>>>|---------source 'Program.cs'  
>>>>>>|---------application 'bin/Release/AmountToWords.exe'  

>>>|----------Project 'AmountToWordsTests'  
>>>>>>|---------source 'AmountToWordsTests.cs'

### References & Build
.NET Framework 4.5.1  
nunit.framework 3.11.0.0   

### UnitTest & Coverage
![](https://github.com/chenhua1008/AmountToWords/blob/master/UnitTestResult.png)


![](https://github.com/chenhua1008/AmountToWords/blob/master/UnitTestCoverage.png)


### Other
microservice:  
[https://www.chenhua.org/mservice/v1/AmountToWords?amount=1.23](https://www.chenhua.org/mservice/v1/AmountToWords?amount=1.23)
