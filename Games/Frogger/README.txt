A console implementation of the popular computer game "Frogger":

* The object of the game is for the player to survive as much as he can. 
* Points are awarded for every movement the player makes with the arrow keys, once on a lane.
* The "traffic" gets progressively faster over time. 
* At random intervals a bonus is generated on a random position. Available bonuses:
	* OneUp bonus - adds a life
	* Slower traffic bonus - slows the traffic a bit
	* Score bonus - gives the player 50 points.
		* While all other bonuses are removed on pickup, this bonus is moved to a random position for the player to take again.
* At random intervals a "hole" is generated on a random position. It disappears on pickup and deducts 2 lifes.