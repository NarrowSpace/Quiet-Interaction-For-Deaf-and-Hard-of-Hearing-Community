from flask import Flask
from adafruit_servokit import ServoKit
import logging

app = Flask(__name__)
kit = ServoKit(channels = 16)

left_right_initial_pos = 90  # Servo 0 for base servo for left and right
up_down_initial_pos = 90    # Servo 1 for top servo for up and down

# Reset servos 0 and 1 to 90 degrees
kit.servo[0].angle = left_right_initial_pos
kit.servo[1].angle = up_down_initial_pos

# Configure logging
logging.basicConfig(level=logging.DEBUG)

@app.route('/up')
def move_up():
    global up_down_initial_pos
    ''' 180 degree as the max angle limit'''
    logging.debug(f"Before moving down: {up_down_initial_pos}")  # Log current position
    
    if up_down_initial_pos <= 180:
        up_down_initial_pos -=5
        kit.servo[1].angle = up_down_initial_pos
        logging.debug(f"After moving down: {up_down_initial_pos}")  # Log after movement
        return "Moved Up"
    
    return "Reached upper limit"

@app.route('/down')
def move_down():
    global up_down_initial_pos
    ''' 0 degree as the min angle limit'''
    if up_down_initial_pos >= 0:
        up_down_initial_pos +=5
        kit.servo[1].angle = up_down_initial_pos
        return "Moved Down"
    return "Reached lower limit"

@app.route('/left')
def move_left():
    global left_right_initial_pos
    logging.debug(f"Before moving left: {left_right_initial_pos}")  # Log current position
    
    if left_right_initial_pos  >=0:
        left_right_initial_pos  += 5
        kit.servo[0].angle = left_right_initial_pos 
        logging.debug(f"After moving left: {left_right_initial_pos}")  # Log current position
        return "Moved Left"

    return "Reached left limit"

@app.route('/right')
def move_right():
    global left_right_initial_pos
    logging.debug(f"Before moving right: {left_right_initial_pos}")  # Log current position
    
    if left_right_initial_pos <= 180:
        left_right_initial_pos -= 5
        kit.servo[0].angle = left_right_initial_pos
        logging.debug(f"After moving right: {left_right_initial_pos}")  # Log current position
        return "Moved Right"
        
    return "Reached right limit"

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8770)
