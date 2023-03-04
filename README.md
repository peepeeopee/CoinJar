# CoinJar

## Intro

This Web API implements a coin jar, which will not allow coins to added once it has reached its capacity.
The jar can also be reset to empty the jar of all coins.

## Data Persistence

Since this assessment is not concerned with underlying 'database', a singleton is used to hold the data in memory

## Completion time

Approximately 2.5hrs

## Comments

The API was written using the minimal API approach where endpoints are registered from the program.cs file and 'old-school'l controllers are not used.

This was done to keep the boiler plate down to a minimum and not add unnecessary built-in features.

I also extended the original ```ICoinJar``` interface to be asynchronous as it is best practice to implement asynchronous request handling for APIs.
