#version 330

uniform vec4 color;
uniform float intensity;

void main()
{
	float r = color[0];
	float g = color[1];
	float b = color[2];
	float a = color[3] * intensity;
	gl_FragColor = vec4(r, g, b, a);
}