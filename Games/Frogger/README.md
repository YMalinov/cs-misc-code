###Frogger

A console implementation of the popular computer game "Frogger", made as a team exercise for Telerik Academy, following the best practices
of object-oriented programming and design:

* The objective of the game is for the player to survive the longest time.
* The game starts when the player gets on a lane (pushes the up-arrow).
* Points are awarded for every movement the player makes with the arrow keys, once the game starts.
* The "traffic" gets progressively faster over time.
* A bonus is generated on a random position, on random intervals of time. The available bonuses are:
  * OneUp bonus - adds a life
	* Slower traffic bonus - slows the traffic a bit
	* Score bonus - gives the player 50 points
		* While all other bonuses are removed on pickup, this bonus is moved to another random position for the player to take again.
* At random intervals a "hole" is generated on a random position. It disappears on pickup and deducts 2 lifes.
