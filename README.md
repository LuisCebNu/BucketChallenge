# BucketChallenge

## Overview
The Water Jug Challenge involves solving the riddle of the same name in which, using two buckets of different capacities, you try and find the optimal solution to measure a specific amount of water.

## How It Works
***
### Input

The API accepts inputs from the user in the form of a POST Method. The name of this inputs are **bucketX**, **bucketY**, and **target**.
Here is an example of a valid Url: 
https://localhost:55067/POST?bucketX=2&bucketY=10&target=4.

### Input Validation

The API performs input validation to make sure the date provided is of the correct data or valid to this use case.
Here are the validation rules for each input:
- **X, Y and Z:** are greater than 0.
-  **X, Y and Z:** are integers (no decimal, fractions)

If the input fails fails to meet this requirements, the server will respond with a ["Invalid input. The values must be integers greater or equal to 1."] message.

### Challenge Solution

I used two different algorithms: **The Bézout's Identity** and **simulation based optimization.**

- Bézout's Identity is use to check wheter two integers **a** and **b** exist two other integers  **x** and **y** in such a way that **ax + by = z or GCD (a, b)**. In the context of the challenge, if the greatest common divisor of the two buckets is divisible by the target amount, then it is possible to measure that target amount using the given buckets. **This is use only to check if the given numbers have a possible solution or not, quicker.**
- Simulation based optimization to try and simulate the pouring and emptying of water bewteen the two buckets until the target amount is reached. Here is a small explanation of the algorithm:
	1. **Initialization:** First create the buckets with their limits and get the target amount.
	2. **Check for impossibilities:** We check if the given set of numbers have a possible, if the target amount is bigger than the maximum capacity of both jugs and which one the buckets is the more efficient one pour from.
	3. **Start of the solution:** After that we fill whichever of the two buckets was decided to be the best for this case. 
	4. **Pouring from bucket x to bucket y:** We then simulate the pouring or transfer bewteen the to buckets. The amount is determine by the limit of bucket x.
	5. **Emptying bucket y:** Bucket y is full at this time, so to continue the process we pour the content out.
	6. **Filling bucket x:** If bucket X is empty, so we fill it up again.
	7. **Repeat until the target is reach:** Once is reach, we send a JSON response with all the steps it took to complete the process.

### Requirements and Usage
- Visual Studio 2022

To use it you can download this repo or clone it by using this command:
```
git clone https://github.com/LuisCebNu/BucketChallenge.git
```

Open it and build it on Visual Studio:
![alt text](https://github.com/LuisCebNu/BucketChallenge/master/Images/solutionexplorer.png?raw=true)
![alt text](https://github.com/LuisCebNu/BucketChallenge/master/Images/build.png?raw=true)

After that, just press F5 or the Run button and you should be ready to go!

## API
***
The API exposes a single endpoint to solve the challenge.

## Endpoint
***
### Url
https://localhost:55067
### Method
POST

## Test API in Postman
***
Use the provided URL and add the variables the following way
bucketX={size of bucket}&bucketY={size of bucket}&target={desired target}.

![alt text](https://github.com/LuisCebNu/BucketChallenge/master/Images/postman.png?raw=true)


#### Note
I couldn't figure out why my computer didn't let me run the xUnit test. But I tested with the following cases:

##### Case 1
```
	**Bucket X 0
	**Bucket Y 0
	**Amount wanted Z 0
```
##### Result
```["Invalid input. The values must be integers greater or equal to 1."]```

##### Case 2
```
	**Bucket X 2
	**Bucket Y 6
	**Amount wanted Z 5
```
##### Result
```
	["No Solution"]
```

##### Case 3
```
	**Bucket X 10
	**Bucket Y 50
	**Amount wanted Z 40
```
##### Result
```
	["Bucket Y is Fill : 50","Bucket Y transfer to Bucket X: X: 10 Y: 40","SOLVED"]
```

##### Case 4
```
	**Bucket X 30
	**Bucket Y 100
	**Amount wanted Z 50
```
##### Result
```
	["No solution"]
```

##### Case 5
```
	**Bucket X 10
	**Bucket Y 40
	**Amount wanted Z 50
```
##### Result
```
	["No solution"]
```

##### Case 6
```
	**Bucket X 500
	**Bucket Y 100
	**Amount wanted Z 200
```
##### Result
```
	[
		"Bucket Y is Fill : 100",
		"Bucket Y transfer to Bucket X: X: 100 Y: 0",
		"Bucket Y is Fill : 100",
		"Bucket Y transfer to Bucket X: X: 200 Y: 0",
		"SOLVED"
	]
```

##### Case 7
```
	Bucket X -10
	Bucket Y 30
	Amount wanted Z 20
```
##### Result
```
	["Invalid input. The values must be integers greater or equal to 1."]
```
