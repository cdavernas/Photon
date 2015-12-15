uniform sampler2D texture;
uniform float threshold;
 
void main()
{
    float factor = 1.0 / (threshold + 0.001);
	vec2 pos = floor(gl_TexCoord[0].xy * factor + 0.5) / factor;
	gl_FragColor = texture2D(texture, pos) * gl_Color;	
}