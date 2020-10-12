# Traffic Lights Decider

Traffic simulation based on our algorithm for traffic lights distribution.

Developed with Unity Version: 2019.f.0f1

Final project part of B.Sc in Ariel University

# Main idea

Traffic is one of the influential concepts nowadays: economy, health, pollution, mental health, time and more all subjected to traffic efficiency.

Therefore it is very important that the roads will allow efficient traffic flow, as junctions are one of the main causes of delays and traffic jams which is our main focus for releasing the congestion on the roads.

We aim for the 'TLD' traffic light algorithm we are developing to be a solution to efficient real-time traffic light distribution. The system today is largely built from statistics methods and not sufficiently affected by real-time information, so on the road many drivers lose precious time that can be dragged into hours of wasted time.

Our algorithm should be efficient enough to handle information and calculation in real time.


# Unity Part - Traffic Simulation

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
