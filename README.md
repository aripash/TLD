# Traffic Lights Decider

Traffic simulation based on our algorithm for traffic lights distribution.

Developed with Unity Version: 2019.f.0f1

Final project part of B.Sc in Ariel University

# Main idea

Traffic is one of the influential concepts nowadays: economy, health, pollution, mental health, time and more all subjected to traffic efficiency.

Therefore it is very important that the roads will allow efficient traffic flow, as junctions are one of the main causes of delays and traffic jams which is our main focus for releasing the congestion on the roads.

We aim for the 'TLD' traffic light algorithm we are developing to be a solution to efficient real-time traffic light distribution. The system today is largely built from statistics methods and not sufficiently affected by real-time information, so on the road many drivers lose precious time that can be dragged into hours of wasted time.

Our algorithm should be efficient enough to handle information and calculation in real time.
![Main idea](/images/TLD.gif)

# Unity Part - Traffic Simulation

We are using Unity to perform the visual representation of the project.
With Unity we can show roads with cars on them and how the algorithm handles all the info and how good the results are based on visual and human judgment.
Unity is a good and easy to use engine and since it works on real-time we can change the info the algorithm has in-real time just like we require to simulate a real junctions and the constant changes that happen on it.

in the following short video we can see how the system works behind the scense, the red diamonds represent the routes, yellow the points the cars need to pass through thier drive, purple are simulated traffic lights(due to time constraint we didnt put a sculpter of one with changing lights), the roads themselfs and the background.
![Unity Part - Traffic Simulation](/images/TLD - road2 - PC, Mac & Linux Standalone - Unity 2019.4.0f1 Personal _DX11_ 2020-10-12 15-29-06.mp4)


# Algorithm - Simulated Annealing

We are using [**Simulated Annealing (wiki)**](https://en.wikipedia.org/wiki/Simulated_annealing) as our main algorithm.

### Algorithm struct description:

The main object is 'Junction' which represent full cycle with _order_ and _density_.
_order_ represents which lane opens up at which part of the cycle.
_density_ represents how dence every lane.

![Algorithm struct description](/images/AlgoMatDesc.png)

The ***order*** is a boolean matrix composed	from lanes and segments, each row represents diffrent lane and each column represents diffrent segment in cycle.

### ***Evaluation function - Eval:*** return the huerist evaluation for openning order which is necessary for picking the best solution.
  **The evaluation proccess:**
  
    1. No constraints
    
    2. All lanes are present within the cycle
    
    3. The more lanes are green in each segment - the better
    
    4. The more lanes with higher overall density are green - the better 

### ***Picking Contender:*** Simulated annealing picks random child and decides if child is better than parent.
  In order to pick child one is randomly created for more effiect storage - randomly pick i,j and change order[i,j].
  after picking child, evaluation is called and the decision is based on results and acceptor function with probabilty (in case new evaluation is worse).
