from flask import Flask, render_template, Response, jsonify
import cv2
import numpy as np
import io

"""
This script creates get access to the html and displaying the captured webcam image on the website
"""
app = Flask(__name__)

@app.route('/')
def index():
    ''' Video streaming page. '''
    return render_template('index.html')

def gen():
    cap = cv2.VideoCapture(0)
    cap.set (3,1280)
    cap.set (4,720)
    while True:
        img, frame = cap.read()
        img, jpeg = cv2.imencode('.jpg', frame)
        frame = jpeg.tobytes()
        yield (b'--frame\r\n'
        b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n')
        
    cap.release()
    cv2.destroyAllWindows()
    
@app.route('/video_feed')
def video_feed():
    """Video streaming route. Put this in the src attribute of an img tag."""
    return Response(gen(),
                    mimetype='multipart/x-mixed-replace; boundary=frame')              

if __name__ == '__main__': 
    app.run(host='0.0.0.0', port = 6600, debug=True, threaded=True)
