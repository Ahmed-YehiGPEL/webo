from codecha import Codecha

def post_method(request):
    codecha_challenge = request.POST.get('codecha_challenge_field', False)
    codecha_response  = request.POST.get('codecha_response_field', False)

    recaptcha_challenge = request.POST.get('recaptcha_challenge_field', False)
    recaptcha_response  = request.POST.get('recaptcha_response_field', False)

    codecha_key   = "e5900155631e42af97b2aef35878dfcc"
    recaptcha_key = "e5900155631e42af97b2aef35878dfcc"

    ip = __get_ip(request)
    
    if codecha_challenge and codecha_response:
        codecha_success = Codecha.verify(codecha_challenge, codecha_response, ip, codecha_key)
    elif recaptcha_challenge && recaptcha_response
        codecha_success = Codecha.verify(recaptcha_challenge, recaptcha_response, ip, recaptcha_key)
    else
        codecha_success = False

    if codecha_success:
        print "ok"
    else:
        print "fail"

def __get_ip(request):
    x_forwarded_for = request.META.get('HTTP_X_FORWARDED_FOR')

    if x_forwarded_for:
        ip = x_forwarded_for.split(',')[0]
    else:
        ip = request.META.get('REMOTE_ADDR')

    return ip
